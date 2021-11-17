using System.Collections.Generic;

namespace Logic.Events
{
    public class GameEventHandlerContainer<T>
    {
        private readonly List<IEventHandler<T>> _eventHandlers;

        public GameEventHandlerContainer()
        {
            _eventHandlers = new List<IEventHandler<T>>();
        }

        public void OnCreateEvent(T context)
        {
            foreach (var handler in _eventHandlers)
                handler.Handle(context);
        }


        public void AddHandler(IEventHandler<T> handler) => _eventHandlers.Add(handler);

        public void Clear() => _eventHandlers.Clear();
    }
}
