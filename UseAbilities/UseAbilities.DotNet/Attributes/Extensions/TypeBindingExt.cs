using System;
using System.Globalization;

namespace UseAbilities.DotNet.Attributes.Extensions
{
    public static class TypeBindingExt
    {
        public static Type BindingWith(this Enum enumValue)
        {
            return StringValueOf(enumValue, typeof(TypeBinding));
        }

        private static Type StringValueOf(IConvertible value, Type descriptionType)
        {
            var fi = value.GetType().GetField(value.ToString(CultureInfo.InvariantCulture));
            var attributes = (TypeBinding[])fi.GetCustomAttributes(descriptionType, false);

            if (attributes.Length > 0) return attributes[0].BindingWith;

            throw new MissingMemberException(string.Format("{0} does not has attribute TypeBinding!", value));
        }
    }
}
