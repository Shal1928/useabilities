using System;
using System.Text;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.ObjectExt;

namespace UseAbilities.Extensions.StringExt
{
    public static class StringBaseExt
    {
        #region Static Elements
        public static readonly string NEW_LINE = "\n";
        public static readonly string NEW_LINE_R = "\r";
        public static readonly string TAB = "\t";

        public static string GenerateSymbols(string symbol, int count)
        {
            var sb = new StringBuilder();
            for (var i = 1; i < count; i++)
                sb.Append(symbol);

            return sb.ToString();
        }
        #endregion

        public static string Paste(this string value, params object[] parameters)
        {
            return String.Format(value, parameters);
        }

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

        public static string GetBetweenTabs(this string value)
        {
            var firstTabIndex = value.IndexOf(TAB, StringComparison.Ordinal);
            var temp = value.Remove(firstTabIndex, 1);
            var nextTabIndex = temp.IndexOf(TAB, StringComparison.Ordinal);

            return value.Copy(firstTabIndex, nextTabIndex);
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

        public static string First(this string value)
        {
            return value.Substring(0,1);
        }

        public static string Last(this string value)
        {
            return value.Substring(value.IndexOfLast(), 1);
        }

        public static string FirstCapital(this string value)
        {
            return String.Format("{0}{1}", value.First().ToUpper(), value.Substring(1, value.IndexOfLast()).ToLower());
        }

        public static string CleanEnd(this string value)
        {
            while (true)
            {
                var last = value.Last();
                if (last == NEW_LINE || last == NEW_LINE_R || last == TAB)
                {
                    value = value.RemoveLast();
                    continue;
                }

                break;
            }

            return value;
        }

        public static string ReplaceFirst(this string value, string oldValue, string newValue)
        {
            var beginIndex = value.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);

            return value.Remove(beginIndex, oldValue.Length).Insert(beginIndex, newValue);
        }

        public static string ClearEdges(this string value, string edge)
        {
            return value.ClearEdges(edge, edge);
        }

        public static string ClearEdges(this string value, string leftEdge, string rightEdge)
        {
            if (leftEdge.NotNull() && value.Substring(0, leftEdge.Length) == leftEdge) value = value.Remove(0, leftEdge.Length);
            if (rightEdge.NotNull() && value.Length >= rightEdge.Length && value.Substring(value.Length - rightEdge.Length, rightEdge.Length) == rightEdge) value = value.Remove(value.Length - rightEdge.Length, rightEdge.Length);

            return value;
        }
    }
}
