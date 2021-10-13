using System.Collections.Generic;
using System.Linq;

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

    public EcsFilter GetFilter<T>() => new EcsFilter(_entities.Where(entity => entity.HasComponent<T>()));
    
    public EcsFilter GetFilter<T, T1>() => new EcsFilter(_entities.Where(entity => 
        entity.HasComponent<T>() && 
        entity.HasComponent<T1>()));
    
    public EcsFilter GetFilter<T, T1, T2>() => new EcsFilter(_entities.Where(entity => 
        entity.HasComponent<T>() && 
        entity.HasComponent<T1>() &&
        entity.HasComponent<T2>()));
    
    public EcsFilter GetFilter<T, T1, T2, T3>() => new EcsFilter(_entities.Where(entity => 
        entity.HasComponent<T>() && 
        entity.HasComponent<T1>() &&
        entity.HasComponent<T2>() &&
        entity.HasComponent<T3>()));
    
    public EcsFilter GetFilter<T, T1, T2, T3, T4>() => new EcsFilter(_entities.Where(entity => 
        entity.HasComponent<T>() && 
        entity.HasComponent<T1>() &&
        entity.HasComponent<T2>() &&
        entity.HasComponent<T3>() &&
        entity.HasComponent<T4>()));
    
    public EcsFilter GetFilter<T, T1, T2, T3, T4, T5>() => new EcsFilter(_entities.Where(entity => 
        entity.HasComponent<T>() && 
        entity.HasComponent<T1>() &&
        entity.HasComponent<T2>() &&
        entity.HasComponent<T3>() &&
        entity.HasComponent<T4>() &&
        entity.HasComponent<T5>()));
}
