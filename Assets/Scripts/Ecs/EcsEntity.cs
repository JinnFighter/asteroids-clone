using System;
using System.Collections.Generic;

namespace Ecs
{
    public class EcsEntity
    {
        private readonly EcsWorld _sourceWorld;
        private readonly Dictionary<Type, int> _componentIndexes;

        public EcsEntity(EcsWorld world)
        {
            _componentIndexes = new Dictionary<Type, int>();
            _sourceWorld = world;
        }

        public ref T GetComponent<T>() where T : struct => ref _sourceWorld.GetComponent<T>(_componentIndexes[typeof(T)]);
        
        public bool HasComponent<T>() where T : struct
        {
            var type = typeof(T);
            return _componentIndexes.ContainsKey(type) && _sourceWorld.HasComponent<T>(_componentIndexes[type]);
        }

        public void AddComponent<T>(in T component) where T : struct
        {
            var type = typeof(T);
            if (_componentIndexes.ContainsKey(type))
                _sourceWorld.ReplaceComponent(component, _componentIndexes[type]);
            else
                _componentIndexes[type] = _sourceWorld.AddComponent(component);
        }

        public void RemoveComponent<T>() where T : struct
        {
            if (HasComponent<T>())
            {
                var type = typeof(T);
                _sourceWorld.RemoveComponent<T>(_componentIndexes[type]);
                _componentIndexes.Remove(type);
            }
        }

        public T TryGetComponent<T>(Func<T> defaultFunc) where T : struct
        {
            if (HasComponent<T>())
                return GetComponent<T>();

            var component = defaultFunc();
            AddComponent(component);
            return component;
        }

        public int GetComponentsCount() => _componentIndexes.Count;
    }
}
