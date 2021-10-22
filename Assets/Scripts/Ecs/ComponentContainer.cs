using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    internal class ComponentContainer<T> where T : struct
    {
        private readonly T[] _components;

        private readonly Queue<int> _freeIds;

        public ComponentContainer()
        {
            _components = new T[] { };
            _freeIds = new Queue<int>();
        }

        public int AddItem(T item)
        {
            int id;
            if (_freeIds.Any())
            {
                id = _freeIds.Dequeue();
                _components[id] = item;
            }
            else
            {
                _components.Append(item);
                id = _components.Length - 1;
            }

            return id;
        }

        public ref T GetItem(int index) => ref _components[index];

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < _components.Length && !_freeIds.Contains(index))
                _freeIds.Enqueue(index);
            else
                throw new IndexOutOfRangeException("Index out of range");
        }

        public void ReplaceItem(T item, int index) => _components[index] = item;

        public int GetComponentsCount() => _components.Length - _freeIds.Count;

        public bool IsAvailable(int index) => index >= 0 && index < _components.Length && !_freeIds.Contains(index);
    }
}
