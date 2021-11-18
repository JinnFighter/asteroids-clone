using System;
using System.Collections.Generic;

namespace Ecs
{
    public class EcsEntity
    {
        private readonly EcsComponentManager _componentManager;
        private readonly Dictionary<Type, int> _componentIndexes;

        internal EcsEntity(EcsComponentManager componentManager)
        {
            _componentManager = componentManager;
            _componentIndexes = new Dictionary<Type, int>();
        }

        public ref T GetComponent<T>() where T : struct => ref _componentManager.GetComponent<T>(_componentIndexes[typeof(T)]);
        
        public bool HasComponent<T>() where T : struct
        {
            var type = typeof(T);
            return _componentIndexes.ContainsKey(type) && _componentManager.HasComponent<T>(_componentIndexes[type]);
        }

        public void AddComponent<T>(in T component) where T : struct
        {
            var type = typeof(T);
            if (_componentIndexes.ContainsKey(type))
                _componentManager.ReplaceComponent(component, _componentIndexes[type]);
            else
                _componentIndexes[type] = _componentManager.AddComponent(component);
        }

        public void RemoveComponent<T>() where T : struct
        {
            if (HasComponent<T>())
            {
                var type = typeof(T);
                _componentManager.RemoveComponent<T>(_componentIndexes[type]);
                _componentIndexes.Remove(type);
            }
        }

        public int GetComponentsCount() => _componentIndexes.Count;
    }
}
