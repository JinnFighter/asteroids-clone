using System;
using System.Collections.Generic;

namespace Ecs
{
    internal class EcsComponentManager
    {
        private readonly Dictionary<Type, object> _componentContainers;

        public EcsComponentManager()
        {
            _componentContainers = new Dictionary<Type, object>();
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
    }
}
