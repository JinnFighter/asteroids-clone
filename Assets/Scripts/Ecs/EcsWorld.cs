using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public class EcsWorld
    {
        private readonly List<EcsEntity> _entities;
        private readonly Dictionary<Type, object> _componentContainers;

        public EcsWorld()
        {
            _entities = new List<EcsEntity>();
            _componentContainers = new Dictionary<Type, object>();
        }

        public EcsEntity CreateEntity()
        {
            var entity = new EcsEntity(this);
            _entities.Add(entity);
            return entity;
        }

        public bool HasComponent<T>(int index) where T : struct
        {
            var hasValue = _componentContainers.TryGetValue(typeof(T), out var obj);
            if (hasValue)
            {
                var container = (ComponentContainer<T>)obj;
                return container.IsAvailable(index);
            }

            return false;
        }

        public int AddComponent<T>(in T component) where T : struct
        {
            var type = typeof(T);
            ComponentContainer<T> componentContainer;
            if (_componentContainers.ContainsKey(type))
                componentContainer = (ComponentContainer<T>)_componentContainers[type];
            else
            {
                componentContainer = new ComponentContainer<T>();
                _componentContainers[type] = componentContainer;
            }

            return componentContainer.AddItem(component);
        }
        
        public void ReplaceComponent<T>(in T component, int index) where T : struct
        {
            var type = typeof(T);
            var componentContainer = (ComponentContainer<T>)_componentContainers[type];

            componentContainer.ReplaceItem(component, index);
        }
        
        public ref T GetComponent<T>(int index) where T : struct
        {
            var type = typeof(T);
            var componentContainer = (ComponentContainer<T>)_componentContainers[type];
            return ref componentContainer.GetItem(index);
        }
        
        public void RemoveComponent<T>(int index) where T : struct
        {
            var type = typeof(T);
            var componentContainer = (ComponentContainer<T>)_componentContainers[type];

            componentContainer.RemoveItem(index);
        }

        public EcsFilter GetFilter<T>() where T: struct => new EcsFilter(_entities.Where(entity => entity.HasComponent<T>()));
    
        public EcsFilter GetFilter<T, T1>() 
            where T : struct
            where T1 : struct => new EcsFilter(_entities.Where(entity => 
            entity.HasComponent<T>() && 
            entity.HasComponent<T1>()));
    
        public EcsFilter GetFilter<T, T1, T2>()
            where T : struct
            where T1 : struct
            where T2 : struct => new EcsFilter(_entities.Where(entity => 
            entity.HasComponent<T>() && 
            entity.HasComponent<T1>() &&
            entity.HasComponent<T2>()));
    
        public EcsFilter GetFilter<T, T1, T2, T3>()
            where T : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct => new EcsFilter(_entities.Where(entity => 
            entity.HasComponent<T>() && 
            entity.HasComponent<T1>() &&
            entity.HasComponent<T2>() &&
            entity.HasComponent<T3>()));
    
        public EcsFilter GetFilter<T, T1, T2, T3, T4>()
            where T : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct => new EcsFilter(_entities.Where(entity => 
            entity.HasComponent<T>() && 
            entity.HasComponent<T1>() &&
            entity.HasComponent<T2>() &&
            entity.HasComponent<T3>() &&
            entity.HasComponent<T4>()));
    
        public EcsFilter GetFilter<T, T1, T2, T3, T4, T5>() 
            where T : struct
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct
            where T5 : struct => new EcsFilter(_entities.Where(entity => 
            entity.HasComponent<T>() && 
            entity.HasComponent<T1>() &&
            entity.HasComponent<T2>() &&
            entity.HasComponent<T3>() &&
            entity.HasComponent<T4>() &&
            entity.HasComponent<T5>()));

        public void RemoveEmptyEntities()
        {
            var emptyEntities = _entities.Where(entity => entity.GetComponentsCount() == 0).ToList();

            foreach (var emptyEntity in emptyEntities)
                _entities.Remove(emptyEntity);
        }

        public void Destroy() => _entities.Clear();
    }
}
