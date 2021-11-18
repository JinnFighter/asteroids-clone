using System.Collections.Generic;
using Ecs.Interfaces;

namespace Ecs
{
    internal class EntityPool : IPool<EcsEntity>
    {
        private readonly Stack<EcsEntity> _entities;

        public EntityPool()
        {
            _entities = new Stack<EcsEntity>();
        }

        public int Count() => _entities.Count;

        public EcsEntity GetItem() => _entities.Pop();

        public void AddItem(EcsEntity item) => _entities.Push(item);
        
        public void Clear() => _entities.Clear();
    }
}
