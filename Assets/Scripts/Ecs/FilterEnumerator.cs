using System;
using System.Collections;
using System.Collections.Generic;

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
