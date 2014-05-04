using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using UseAbilities.Extensions.StringExt;

namespace UseAbilities.Extensions.ObjectExt
{
    public static class ObjectBaseExt
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool NotNull(this object obj)
        {
            return obj != null;
        }

        public static T GetValue<T>(this object obj, string propertyName)
        {
            if(propertyName.IsNullOrEmptyOrSpaces()) throw new InvalidDataException("Property name is invalid!");
            var info = obj.GetType().GetProperty(propertyName);
            return (T)info.GetValue(obj, null);
        }

        //public static T GetValue<T>(this object obj, Expression<Func<T>> propertySelector)
        //{
        //    var memberExpression = propertySelector.Body as MemberExpression;
        //    if (memberExpression == null) throw new InvalidExpressionException("PropertySelector is null or not MemberExpression!");

        //    return obj.GetValue<T>(memberExpression.Member.Name);
        //}


        //TODO: Make with Expression safe call Props or Meths with null check
        //public static bool SafeCheck(this object obj, )
    }
}
