using System.Collections.Generic;

namespace AniraSP.Utilities {
    public static class DictionaryExtensions {
        public static bool AddIfNotExists<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key, TVal val) {
            if (dict == null) return false;
            if (dict.ContainsKey(key)) return false;

            dict.Add(key, val);

            return true;
        }

        public static TVal GetValueIfExist<TKey, TVal>(this Dictionary<TKey, TVal> dict, TKey key) {
            if (dict == null) return default(TVal);
            if (!dict.ContainsKey(key)) return default(TVal);

            return dict[key];
        }
    }
}