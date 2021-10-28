using System.Collections.Generic;
using Random = System.Random;

namespace DataContainers
{
    public abstract class RandomObjectSelector<T> : IObjectSelector<T>
    {
        private readonly List<T> _items;
        private readonly Random _random;

        protected RandomObjectSelector(IEnumerable<T> items)
        {
            _items = new List<T>(items);
            _random = new Random();
        }

        public T GetObject() => _items[_random.Next(0, _items.Count)];
    }
}
