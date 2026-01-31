using System.Collections.Generic;
using UnityEngine;

namespace TEXADev.SerializedTypes
{
    [System.Serializable]
    public class SerializedDictionary<TKey, TValue>
    {
        [SerializeField] private List<DictionaryElement<TKey, TValue>> dictionary;

        public int Count { get => dictionary.Count; }
        public TKey[] Keys { get => getKeys(); }
        public TValue[] Values { get => getValues(); }

        private TKey[] getKeys()
        {
            TKey[] keys = new TKey[dictionary.Count];
            for (int i = 0; i < dictionary.Count; i++)
            {
                keys[i] = dictionary[i].Key;
            }
            return keys;
        }

        private TValue[] getValues()
        {
            TValue[] values = new TValue[dictionary.Count];
            for (int i = 0; i < dictionary.Count; i++)
            {
                values[i] = dictionary[i].Value;
            }
            return values;
        }

        public void Add(TKey key, TValue value)
        {
            if (!HasKey(key))
                dictionary.Add(new DictionaryElement<TKey, TValue>(key, value));
        }

        public TValue Get(TKey key)
        {
            foreach (DictionaryElement<TKey, TValue> i in dictionary)
            {
                if (i.Key.Equals(key))
                    return i.Value;
            }
            return default(TValue);
        }

        public bool HasKey(TKey key)
        {
            foreach (DictionaryElement<TKey, TValue> i in dictionary)
            {
                if (i.Key.Equals(key))
                    return true;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get => Get(key);
        }
    }
}
