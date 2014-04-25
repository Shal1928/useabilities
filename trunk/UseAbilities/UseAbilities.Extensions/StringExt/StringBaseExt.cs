using System;
using UseAbilities.Extensions.EnumerableExt;

namespace UseAbilities.Extensions.StringExt
{
    public static class StringBaseExt
    {
        public static bool IsNullOrEmptyOrSpaces(this string value)
        {
            return String.IsNullOrWhiteSpace(value) || value.IsEmpty();
        }
    }
}
