using System.Collections.Generic;

public class World
{
    private readonly List<EcsEntity> _entities;

    public World()
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
