namespace Logic.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T context);
    }
}
