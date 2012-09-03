using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UseAbilities.DotNet.Reflection
{
    public static class Instance
    {
        private const BindingFlags BINDING_FLAGS_ALL_INSTANCE_CTORS = BindingFlags.NonPublic |
                                                                      BindingFlags.Public |
                                                                      BindingFlags.Instance;

        public static object Create(ConstructorInfo ctor, params object[] args)
        {
            return CreateInstance(ctor, args);
        }
        
        public static object Create(Type type, params object[] args)
        {
            var ctor = type.GetConstructors(BINDING_FLAGS_ALL_INSTANCE_CTORS).First();
            return CreateInstance(ctor, args);
        }

        public static object Create(Type genericType, Type type, params object[] args)
        {
            Type[] types = {type};
            var generic = genericType.MakeGenericType(types);
            var ctor = generic.GetConstructors(BINDING_FLAGS_ALL_INSTANCE_CTORS).First();
            return CreateInstance(ctor, args);
        }

        private static object CreateInstance(ConstructorInfo ctor, params object[] args)
        {
            var newExp = Expression.New(ctor, args.Select(Expression.Constant));
            var lambda = Expression.Lambda(typeof(Func<object>), newExp);
            var compiled = (Func<object>)lambda.Compile();

            var instance = compiled();
            return instance;
        }
    }
}
