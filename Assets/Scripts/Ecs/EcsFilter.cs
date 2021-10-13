using System.Collections.Generic;

public class EcsFilter
{
    private List<EcsEntity> _entities;

    public EcsFilter(IEnumerable<EcsEntity> entities)
    {
        _entities = new List<EcsEntity>(entities);
    }
}
