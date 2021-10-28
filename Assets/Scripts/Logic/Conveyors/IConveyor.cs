namespace Logic.Conveyors
{
    public interface IConveyor<T>
    {
        void UpdateItem(T item);
        IConveyor<T> AddNextConveyor(IConveyor<T> nextConveyor);
    }
    
    public interface IConveyor<T, T1>
    {
        void UpdateItem(T item, T1 param);
        IConveyor<T, T1> AddNextConveyor(IConveyor<T, T1> nextConveyor);
    }
}
