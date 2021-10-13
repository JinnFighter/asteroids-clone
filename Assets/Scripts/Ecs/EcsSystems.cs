using System;
using System.Collections.Generic;
using System.Linq;

public class EcsSystems
{
    private readonly EcsWorld _world;

    private readonly List<IEcsRunSystem> _runSystems;
    private readonly List<IEcsRunSystem> _removeOneFrameSystems;
    private readonly Dictionary<Type, object> _services;
    
    private readonly HashSet<Type> _oneFrameTypes;

    public EcsSystems(EcsWorld world)
    {
        _world = world;
        _runSystems = new List<IEcsRunSystem>();
        _removeOneFrameSystems = new List<IEcsRunSystem>();
        _services = new Dictionary<Type, object>();
        _oneFrameTypes = new HashSet<Type>();
    }

    public EcsSystems AddSystem(IEcsRunSystem runSystem)
    {
        _runSystems.Add(runSystem);
        return this;
    }
    
    public EcsSystems AddDataCollection(object obj)
    {
        _services[obj.GetType()] = obj;
        return this;
    }

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

        foreach (var removeOneFrameSystem in _removeOneFrameSystems)
            removeOneFrameSystem.Run(_world);

        _world.Cleanup();
    }

    public EcsSystems OneFrame<T>() where T : struct
    {
        _removeOneFrameSystems.Add(new RemoveOneFrameSystem<T>());
        return this;
    }
}
