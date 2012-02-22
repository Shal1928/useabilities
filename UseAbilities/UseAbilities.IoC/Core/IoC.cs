using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UseAbilities.IoC.Attributes;

namespace UseAbilities.IoC.Core
{
    public class IoC
    {
        private const BindingFlags BINDING_FLAGS_ALL_INSTANCE_CTORS = BindingFlags.NonPublic | 
                                                                      BindingFlags.Public | 
                                                                      BindingFlags.Instance;

        private readonly IDictionary<Type, RegisteredObject> _registeredObjects = new Dictionary<Type, RegisteredObject>();

        public void Register<TType>() where TType : class
        {
            Register<TType, TType>(false, null);
        }

        public void Register<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(false, null);
        }

        public void RegisterSingleton<TType>() where TType : class
        {
            RegisterSingleton<TType, TType>();
        }

        public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, null);
        }

        public void RegisterInstance<TType>(TType instance) where TType : class
        {
            RegisterInstance<TType, TType>(instance);
        }

        public void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, instance);
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve) ResolveObject(typeof (TTypeToResolve));
        }

        public object Resolve(Type type)
        {
            return ResolveObject(type);
        }

        private void Register<TType, TConcrete>(bool isSingleton, TConcrete instance)
        {
            Register(typeof (TType), typeof (TConcrete), isSingleton, instance);
        }

        private void Register(Type type, Type concreteType, bool isSingleton, object concreteInstance)
        {
            if (_registeredObjects.ContainsKey(type)) _registeredObjects.Remove(type);

            var registeredObject = new RegisteredObject(concreteType, isSingleton, concreteInstance);
            _registeredObjects.Add(type, registeredObject);
        }

        private object ResolveObject(Type type)
        {
            if (type == GetType())
                return this;

            if (!_registeredObjects.ContainsKey(type))
            {
                if (!type.IsInterface && !type.IsAbstract) Register(type, type, false, null);
                else
                    throw new ArgumentOutOfRangeException(string.Format("The type {0} has not been registered",
                                                                        type.Name));
            }

            return GetInstance(_registeredObjects[type]);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            var instance = registeredObject.Instance;
            if (instance == null)
            {
                var injectedProperties = registeredObject.ConcreteType.GetProperties(BINDING_FLAGS_ALL_INSTANCE_CTORS);
                var properties = injectedProperties.Where(injectedProperty =>
                                                          injectedProperty.GetCustomAttributes(
                                                              typeof (InjectedProperty), false).Any()).ToList();

                if (properties.Any())
                {
                    var propertiesMappingDic = properties.ToDictionary(property => property, 
                                                                       property => ResolveObject(property.PropertyType));

                    instance = registeredObject.CreateInstance(propertiesMappingDic);
                }
                else
                {
                    var constructorInfo =
                        registeredObject.ConcreteType.GetConstructors(BINDING_FLAGS_ALL_INSTANCE_CTORS).First();
                    var parameters =
                        constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
                    instance = registeredObject.CreateInstance(constructorInfo, parameters.ToArray());
                }
            }

            return instance;
        }
    }
}
