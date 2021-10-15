namespace Logic.Conveyors
{
    public abstract class Conveyor<T> : IConveyor<T>
    {
        private IConveyor<T> _nextConveyor;
        
        public void UpdateItem(T item)
        {
            UpdateItemInternal(item);
            _nextConveyor?.UpdateItem(item);
        }

        protected abstract void UpdateItemInternal(T item);

        public IConveyor<T> AddNextConveyor(IConveyor<T> nextConveyor)
        {
            _nextConveyor = nextConveyor;
            return nextConveyor;
        }
    }
}
