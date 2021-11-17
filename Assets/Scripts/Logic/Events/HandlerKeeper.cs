using System;
using System.Collections.Generic;

namespace Logic.Events
{
    public abstract class HandlerKeeper<T>
    {
        private readonly Dictionary<Type, GameEventHandlerContainer<T>> _dictionary;

        public HandlerKeeper()
        {
            _dictionary = new Dictionary<Type, GameEventHandlerContainer<T>>();
        }

        public void HandleEvent<C>(T context) where C : struct
        {
            if(_dictionary.TryGetValue(typeof(C), out var handlerContainer))
                handlerContainer.OnCreateEvent(context);
        }

        public void AddHandler<C>(IEventHandler<T> handler) where C : struct
        {
            GameEventHandlerContainer<T> handlerContainer;
            var key = typeof(C);
            if (_dictionary.ContainsKey(key))
                handlerContainer = _dictionary[key];
            else
            {
                handlerContainer = new GameEventHandlerContainer<T>();
                _dictionary[key] = handlerContainer;
            }
            
            handlerContainer.AddHandler(handler);
        }

        public void Clear()
        {
            foreach (var container in _dictionary)
                container.Value.Clear();
            _dictionary.Clear();
        }
    }
}
