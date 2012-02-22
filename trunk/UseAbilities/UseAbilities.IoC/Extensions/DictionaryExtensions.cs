using System.Collections.Generic;
using System.Reflection;

namespace UseAbilities.IoC.Extensions
{
    internal static class DictionaryExtensions
    {
        public static T GetValue<T>(this Dictionary<PropertyInfo, object> d, PropertyInfo propertyInfo)
        {
            object value;
            return d.TryGetValue(propertyInfo, out value) ? (T)value : default(T);
        }
    }
}
