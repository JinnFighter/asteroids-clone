namespace Logic.Events
{
    public interface IEventHandler<T>
    {
        void OnCreateEvent(T context);
        void OnDestroyEvent(T context);
    }
}
