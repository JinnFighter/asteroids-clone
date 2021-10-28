namespace DataContainers
{
    public abstract class SingleObjectSelector<T> : IObjectSelector<T>
    {
        private readonly T _obj;

        protected SingleObjectSelector(T obj)
        {
            _obj = obj;
        }

        public T GetObject() => _obj;
    }
}
