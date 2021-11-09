using System.Collections.Generic;
using Helpers;

namespace DataContainers
{
    public abstract class RandomObjectSelector<T> : IObjectSelector<T>
    {
        private readonly List<T> _items;
        private readonly IRandomizer _randomizer;

        protected RandomObjectSelector(IEnumerable<T> items, IRandomizer randomizer)
        {
            _items = new List<T>(items);
            _randomizer = randomizer;
        }

        public T GetObject() => _items[_randomizer.Range(0, _items.Count)];
    }
}
