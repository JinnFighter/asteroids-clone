using System.Collections.Generic;

namespace Logic.Events
{
    public abstract class GameEventHandlerContainer<T>
    {
        private readonly List<IEventHandler<T>> _eventHandlers;

        protected GameEventHandlerContainer()
        {
            _eventHandlers = new List<IEventHandler<T>>();
        }

        public void OnCreateEvent(T context)
        {
            foreach (var handler in _eventHandlers)
                handler.OnCreateEvent(context);
        }
        
        public void OnDestroyEvent(T context)
        {
            foreach (var handler in _eventHandlers)
                handler.OnDestroyEvent(context);
        }


        public void AddHandler(IEventHandler<T> handler) => _eventHandlers.Add(handler);

        public void Clear() => _eventHandlers.Clear();
    }
}
