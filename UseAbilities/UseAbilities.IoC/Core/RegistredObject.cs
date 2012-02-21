using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common.Code.UseAbilities.IoC.Extensions;

namespace Common.Code.UseAbilities.IoC.Core
{
    internal class RegisteredObject
    {
        private readonly bool _isSinglton;

        public RegisteredObject(Type concreteType, bool isSingleton, object instance)
        {
            _isSinglton = isSingleton;
            ConcreteType = concreteType;
            Instance = instance;
        }

        public Type ConcreteType
        {
            get; 
            private set;
        }

        public object Instance
        {
            get; 
            private set;
        }

        public object CreateInstance(Dictionary<PropertyInfo, object> propertiesMappingDic)
        {
            var newExp = Expression.New(ConcreteType);
            var dictParam = Expression.Parameter(typeof(Dictionary<PropertyInfo, object>), "d");
            var memberBindingList = (from propertyInfo in propertiesMappingDic.Keys
                                     let call = Expression.Call(typeof (DictionaryExtensions), 
                                                                "GetValue", 
                                                                new[]{propertyInfo.PropertyType}, 
                                                                new Expression[]{dictParam, Expression.Constant(propertyInfo)})//let call
                                     select Expression.Bind(propertyInfo.GetSetMethod(), call)).Cast<MemberBinding>().ToList();

            var lambda = Expression.Lambda<Func<Dictionary<PropertyInfo, object>, object>>(Expression.MemberInit(newExp, memberBindingList), new[] { dictParam });

            var compiled = lambda.Compile();

            var instance = compiled(propertiesMappingDic);
            if (_isSinglton) Instance = instance;
            return instance;
        }

        public object CreateInstance(ConstructorInfo ctor, params object[] args)
        {
            var newExp = Expression.New(ctor, args.Select(Expression.Constant));
            var lambda = Expression.Lambda(typeof(Func<object>), newExp); //()=>new SettingsStore
            var compiled = (Func<object>)lambda.Compile();

            var instance = compiled();
            if (_isSinglton) Instance = instance;
            return instance;
        }
    }
}
