using System.Collections.Generic;
using System.Linq;

namespace UseAbilities.Extensions.EnumerableExt
{
    public static class FrequentlyValuesExt
    {
        public static IEnumerable<int> GetFrequentlyValues(this IEnumerable<int> enumerable, int topCount)
        {
            return enumerable
                   .GroupBy(i => i)
                   .OrderByDescending(g => g.Count())
                   .Take(topCount)
                   .Select(g => g.Key);
        }

        //public static T GetFrequentlyValue<T>(this IEnumerable<T> enumerable)
        //{
        //    return enumerable
        //           .GroupBy(i => i)
        //           .OrderByDescending(g => g.Count())
        //           .Take(1)
        //           .Select(g => g.Key)
        //           .FirstOrDefault();
        //}
    }
}
