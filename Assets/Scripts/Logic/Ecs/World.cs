using System.Collections.Generic;

public class World
{
    private readonly List<Entity> _entities;

    public World()
    {
        _entities = new List<Entity>();
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();
        _entities.Add(entity);
        return entity;
    }
}
