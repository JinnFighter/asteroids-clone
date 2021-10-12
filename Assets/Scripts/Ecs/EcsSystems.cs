using System;
using System.Collections.Generic;
using System.Linq;

public class EcsSystems
{
    private readonly EcsWorld _world;

    private readonly List<IEcsRunSystem> _runSystems;
    private readonly Dictionary<Type, object> _services;

    public EcsSystems(EcsWorld world)
    {
        _world = world;
        _runSystems = new List<IEcsRunSystem>();
        _services = new Dictionary<Type, object>();
    }

    public EcsSystems AddSystem(IEcsRunSystem runSystem)
    {
        _runSystems.Add(runSystem);
        return this;
    }
    
    public void AddDataCollection(object obj) => _services[obj.GetType()] = obj;

    public T GetDataCollection<T>()
    {
        var hasValue = _services.TryGetValue(typeof(T), out var res);
        if (hasValue)
            return (T) res;

        return (T) _services.Values.FirstOrDefault(val => val is T);
    }

    public void Run()
    {
        foreach (var system in _runSystems)
            system.Run(_world);
    }
}
