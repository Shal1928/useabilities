using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UseAbilities.IoC.Extensions;

namespace UseAbilities.IoC.Core
{
    public static class Extensions
    {
        public static MethodInfo GetSetMethodOnDeclaringType(this PropertyInfo propertyInfo)
        {
            var methodInfo = propertyInfo.GetSetMethod(true);
            return methodInfo ?? propertyInfo
                                    .DeclaringType
                                    .GetProperty(propertyInfo.Name)
                                    .GetSetMethod(true);
        }
    }

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
            //var memberBindingList = (from propertyInfo in propertiesMappingDic.Keys
            //                         let call = Expression.Call(typeof (DictionaryExtensions), 
            //                                                    "GetValue", 
            //                                                    new[]{propertyInfo.PropertyType}, 
            //                                                    new Expression[]{dictParam, Expression.Constant(propertyInfo)})//let call
            //                         select Expression.Bind(propertyInfo.GetSetMethod(), call)).Cast<MemberBinding>().ToList();


            //var memberBindingList = (from propertyInfo in propertiesMappingDic.Keys let call = Expression.Call(typeof(DictionaryExtensions), "GetValue", new[] { propertyInfo.PropertyType }, new Expression[] { dictParam, Expression.Constant(propertyInfo) })//let call
            //                         select Expression.Bind(propertyInfo.GetSetMethod(), call)).Cast<MemberBinding>().ToList();

            //For ObserveWrapper
            //var memberBindingList = new List<MemberBinding>();

            //foreach (var propertyInfo in propertiesMappingDic.Keys)
            //{
            //    var expression = new Expression[] { dictParam, Expression.Constant(propertyInfo) };
            //    var array = new[] { propertyInfo.PropertyType };

            //    var call = Expression.Call(typeof(DictionaryExtensions), "GetValue", array, expression);
            //    //var setMethod = propertyInfo.GetSetMethod();

            //    var memberBinding = Expression.Bind(propertyInfo, call);
            //    memberBindingList.Add(memberBinding);
            //}

            var memberBindingList = (from propertyInfo in propertiesMappingDic.Keys
                                     let call = Expression.Call(typeof(DictionaryExtensions),
                                                                "GetValue",
                                                                new[] { propertyInfo.PropertyType },
                                                                new Expression[] { dictParam, Expression.Constant(propertyInfo) })//let call
                                     select Expression.Bind(propertyInfo, call)).Cast<MemberBinding>().ToList();
            
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
