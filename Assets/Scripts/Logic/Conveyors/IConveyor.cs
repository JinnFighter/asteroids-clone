namespace Logic.Conveyors
{
    public interface IConveyor<T>
    {
        void UpdateItem(T item);
        IConveyor<T> AddNextConveyor(IConveyor<T> nextConveyor);
    }
}
