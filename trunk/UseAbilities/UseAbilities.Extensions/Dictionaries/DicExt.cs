using System;
using System.Collections.Generic;
using UseAbilities.Extensions.StringExt;

namespace UseAbilities.Extensions.Dictionaries
{
    public static class DicExt
    {
        public static void Put<TKey, TVal>(this IDictionary<TKey, TVal> dic, TKey key, TVal value)
        {
            if (dic.ContainsKey(key)) dic[key] = value;
            else dic.Add(key, value);
        }


        public static void UniqPut<TVal>(this IDictionary<string, TVal> dic, string key, TVal value, string separator = "_")
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, value);
                return;
            }

            if (key.Contains(separator))
            {
                var oldSuffixStr = key.Copy(key.IndexOfNext(separator), key.IndexOfLast());
                int oldSuffix;
                if (int.TryParse(oldSuffixStr, out oldSuffix))
                {
                    var cleanKey = key.Copy(0, key.IndexOfPreview(separator));
                    oldSuffix++;
                    dic.UniqPut(String.Format("{0}{1}{2}", cleanKey, separator, oldSuffix), value);
                    return;
                }
                
                throw new ArgumentException("Separator not unique element in key!");
            }

            dic.UniqPut(String.Format("{0}{1}{2}", key, separator, 1), value);
        }
    }
}
