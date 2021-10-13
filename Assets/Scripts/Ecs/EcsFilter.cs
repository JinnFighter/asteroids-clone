using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EcsFilter : IEnumerable<EcsEntity>
{
    private readonly List<EcsEntity> _entities;

    public EcsFilter(IEnumerable<EcsEntity> entities)
    {
        _entities = new List<EcsEntity>(entities);
    }

    public EcsFilter Exclude<T>() => new EcsFilter(_entities.Where(entity => !entity.HasComponent<T>()));
    
    public EcsFilter Exclude<T, T1>() => new EcsFilter(_entities.Where(entity => !(entity.HasComponent<T>() || entity.HasComponent<T1>())));

    public IEnumerator<EcsEntity> GetEnumerator() => new FilterEnumerator(_entities);

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class FilterEnumerator : IEnumerator<EcsEntity>
{
    private readonly List<EcsEntity> _entities;
    private int _position;

    public FilterEnumerator(IEnumerable<EcsEntity> entities)
    {
        _entities = new List<EcsEntity>(entities);
        _position = -1;
    }

    public bool MoveNext()
    {
        _position++;
        return _position < _entities.Count;
    }

    public void Reset() => _position = -1;

    public EcsEntity Current
    {
        get
        {
            try
            {
                return _entities[_position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("Index out of range");
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose() => _entities.Clear();
}
