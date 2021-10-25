using System.Collections.Generic;

namespace DataContainers
{
    public abstract class DictionaryDataContainer<T, T1> : IDataContainer<T, T1>
    {
        private readonly Dictionary<T, T1> _dictionary;

        public DictionaryDataContainer()
        {
            _dictionary = new Dictionary<T, T1>();
        }

        public void AddData(T key, T1 value) => _dictionary.Add(key, value);

        public T1 GetData(T key) => _dictionary[key];

        public bool Contains(T1 item) => _dictionary.ContainsValue(item);

        public bool TryGetValue(T key, out T1 value) => _dictionary.TryGetValue(key, out value);
    }
}
