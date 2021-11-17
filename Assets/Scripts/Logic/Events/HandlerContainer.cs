using System;
using System.Collections.Generic;

namespace Logic.Events
{
    public abstract class HandlerContainer<T>
    {
        private readonly Dictionary<Type, List<IEventHandler<T>>> _dictionary;

        public HandlerContainer()
        {
            _dictionary = new Dictionary<Type, List<IEventHandler<T>>>();
        }

        public void HandleEvent<C>(T context) where C : struct
        {
            foreach (var handler in _dictionary[typeof(C)])
                handler.Handle(context);
        }

        public void AddHandler<C>(IEventHandler<T> handler) where C : struct
        {
            List<IEventHandler<T>> handlers;
            var key = typeof(C);
            if (_dictionary.ContainsKey(key))
                handlers = _dictionary[key];
            else
            {
                handlers = new List<IEventHandler<T>>();
                _dictionary[key] = handlers;
            }
            
            handlers.Add(handler);
        }

        public void Clear() => _dictionary.Clear();
    }
}
