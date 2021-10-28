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

        public IConveyor<T> GetLast() => _nextConveyor == null ? this : _nextConveyor.GetLast();
    }
    
    public abstract class Conveyor<T, T1> : IConveyor<T, T1>
    {
        private IConveyor<T, T1> _nextConveyor;
        
        public void UpdateItem(T item, T1 param)
        {
            UpdateItemInternal(item, param);
            _nextConveyor?.UpdateItem(item, param);
        }

        protected abstract void UpdateItemInternal(T item, T1 param);

        public IConveyor<T, T1> AddNextConveyor(IConveyor<T, T1> nextConveyor)
        {
            _nextConveyor = nextConveyor;
            return nextConveyor;
        }

        public IConveyor<T, T1> GetLast() => _nextConveyor == null ? this : _nextConveyor.GetLast();
    }
}
