namespace DataContainers
{
    public interface IDataContainer<T, T1>
    {
        void AddData(T key, T1 value);
        T1 GetData(T key);
        bool Contains(T1 item);
        bool TryGetValue(T key, out T1 value);
    }
}
