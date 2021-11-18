using System;
using System.Collections.Generic;

namespace Logic.Events
{
    public class ComponentEventHandlerContainer
    {
        private readonly Dictionary<Type, List<object>> _dictionary;

        public ComponentEventHandlerContainer()
        {
            _dictionary = new Dictionary<Type, List<object>>();
        }

        public void HandleEvent<T>(ref T context) where T : struct
        {
            foreach (IComponentEventHandler<T> handler in _dictionary[typeof(T)])
            {
                handler.Handle(ref context);
            }
        }

        public void AddHandler<T>(IComponentEventHandler<T> handler) where T : struct
        {
            List<object> handlers;
            var key = typeof(T);
            if (_dictionary.ContainsKey(key))
                handlers = _dictionary[key];
            else
            {
                handlers = new List<object>();
                _dictionary[key] = handlers;
            }
            
            handlers.Add(handler);
        }

        public void Clear() => _dictionary.Clear();
    }
}
