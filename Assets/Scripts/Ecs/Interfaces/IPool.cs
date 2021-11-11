namespace Ecs.Interfaces
{
    internal interface IPool<T>
    {
        int Count();
        T GetItem();
        void AddItem(T item);
    }
}
