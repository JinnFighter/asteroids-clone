using System.Collections.Generic;

public class EcsWorld
{
    private readonly List<EcsEntity> _entities;

    public EcsWorld()
    {
        _entities = new List<EcsEntity>();
    }

    public EcsEntity CreateEntity()
    {
        var entity = new EcsEntity();
        _entities.Add(entity);
        return entity;
    }
}
