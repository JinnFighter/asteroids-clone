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

        public void HandleEvent<C>(T context) where C : struct => _dictionary[typeof(C)].OnCreateEvent(context);

        public void AddHandlerContainer<C>(GameEventHandlerContainer<T> container) where C : struct =>
            _dictionary[typeof(C)] = container;

        public void Clear()
        {
            foreach (var container in _dictionary)
                container.Value.Clear();
            _dictionary.Clear();
        }
    }
}
