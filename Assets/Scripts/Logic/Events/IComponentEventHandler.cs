namespace Logic.Events
{
    public interface IComponentEventHandler<T> where T : struct
    {
        void Handle(ref T context);
    }
}
