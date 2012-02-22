using System;
using System.ComponentModel;
using System.Globalization;

namespace UseAbilities.Extensions.Enums
{
    public static class DescriptionOfExt
    {
        //public static object EnumValueOf(this Enum enumValue, string value)
        //{
        //    var enumType = enumValue.GetType();
        //    var names = Enum.GetNames(enumType);
        //    foreach (var name in names.Where(name => DescriptionOf(Enum.Parse(enumType, name)).Equals(value)))
        //        return Enum.Parse(enumType, name);


        //    throw new ArgumentException("The string is not a description or value of the specified enum.");
        //}

        public static string DescriptionOf(this Enum enumValue)
        {
            return StringValueOf(enumValue, typeof(DescriptionAttribute));
        }

        private static string StringValueOf(IConvertible value, Type descriptionType)
        {
            var fi = value.GetType().GetField(value.ToString(CultureInfo.InvariantCulture));
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(descriptionType, false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
