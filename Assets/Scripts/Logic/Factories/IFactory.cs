namespace Logic.Factories
{
    public interface IFactory<T>
    {
        T CreateObject();
    }
}
