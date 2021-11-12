using System;
using System.Collections.Generic;

namespace Logic.Events
{
    public class ComponentEventListener
    {
        private Dictionary<Type, List<object>> _dictionary;

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
    }
}
