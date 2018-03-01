using System.Collections.Generic;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    static class DictionaryExtensions
    {
        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }
    }
}
