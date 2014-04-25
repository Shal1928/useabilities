using System.Collections.Generic;

namespace UseAbilities.Extensions.Dictionaries
{
    public static class DicExt
    {
        public static void Put<TKey, TVal>(this Dictionary<TKey, TVal> dic, TKey key, TVal value)
        {
            if (dic.ContainsKey(key)) dic[key] = value;
            else dic.Add(key, value);
        }
    }
}
