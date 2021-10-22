using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecs
{
    public class EcsEntity
    {
        private readonly EcsWorld _sourceWorld;
        private readonly Dictionary<Type, object> _components;
        private readonly Dictionary<Type, int> _componentIndexes;

        public EcsEntity(EcsWorld world)
        {
            _components = new Dictionary<Type, object>();
            _componentIndexes = new Dictionary<Type, int>();
            _sourceWorld = world;
        }

        public T GetComponent<T>() where T : struct
        {
            var hasValue = _components.TryGetValue(typeof(T), out var res);
            if (hasValue)
                return (T)res;

            return (T)_components.Values.FirstOrDefault(val => val is T);
        }
        
        public T GetComponento<T>() where T : struct => _sourceWorld.GetComponent<T>(_componentIndexes[typeof(T)]);

        public bool HasComponent<T>() where T : struct
        {
            var type = typeof(T);
            return _components.ContainsKey(type) && _components[type] != null || _components.Values.Any(val => val is T);
        }
        
        public bool HasComponento<T>() where T : struct
        {
            var type = typeof(T);
            return _components.ContainsKey(type) && _sourceWorld.HasComponent<T>(_componentIndexes[type]);
        }
        
        public void AddComponent<T>(in T component) where T : struct => _components[component.GetType()] = component;

        public void AddComponento<T>(in T component) where T : struct
        {
            var type = typeof(T);
            if (_componentIndexes.ContainsKey(type))
                _sourceWorld.ReplaceComponent(component, _componentIndexes[type]);
            else
                _componentIndexes[typeof(T)] = _sourceWorld.AddComponent(component);
        }

        public void RemoveComponent<T>() where T : struct
        {
            if (HasComponent<T>())
                _components.Remove(typeof(T));
        }
        
        public void RemoveComponento<T>() where T : struct
        {
            if (HasComponento<T>())
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
        
        public T TryGetComponento<T>(Func<T> defaultFunc) where T : struct
        {
            if (HasComponento<T>())
                return GetComponento<T>();

            var component = defaultFunc();
            AddComponento(component);
            return component;
        }

        public int GetComponentsCount() => _components.Values.Count;
    }
}
