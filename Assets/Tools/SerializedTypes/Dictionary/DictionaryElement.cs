namespace TEXADev.SerializedTypes
{
    [System.Serializable]
    public class DictionaryElement<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public DictionaryElement(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
