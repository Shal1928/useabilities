﻿using System;
using UseAbilities.Extensions.EnumerableExt;

namespace UseAbilities.Extensions.StringExt
{
    public static class StringBaseExt
    {
        public static readonly string NEW_LINE = "\n";

        public static bool IsNullOrEmptyOrSpaces(this string value)
        {
            return String.IsNullOrWhiteSpace(value) || value.IsEmpty();
        }

        public static string Copy(this string value, int beginIndex, int endIndex)
        {
            return value.Substring(beginIndex, endIndex - beginIndex + 1);
        }

        public static string RemoveLast(this string value)
        {
            return value.Remove(value.IndexOfLast());
        }

        public static string GetAfterNewLine(this string value)
        {
            var newLineIndex = value.IndexOfNext(NEW_LINE);
            return value.Copy(newLineIndex, value.IndexOfLast());
        }

        public static int IndexOfNext(this string value, string item, StringComparison comparison = StringComparison.Ordinal)
        {
            return value.IndexOf(item, comparison) + 1;
        }

        public static int IndexOfPreview(this string value, string item, StringComparison comparison = StringComparison.Ordinal)
        {
            return value.IndexOf(item, comparison) - 1;
        }

        public static int IndexOfLast(this string value)
        {
            return value.Length - 1;
        }
    }
}
