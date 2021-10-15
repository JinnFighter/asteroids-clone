using System;
using System.Collections.Generic;
using System.Linq;
using Ecs.Interfaces;
using Ecs.Systems;

namespace Ecs
{
    public class EcsSystems
    {
        private readonly EcsWorld _world;

        private readonly Queue<IEcsInitSystem> _initSystems;
        private readonly List<IEcsRunSystem> _runSystems;
        private readonly List<IEcsRunSystem> _removeOneFrameSystems;
        private readonly Dictionary<Type, object> _services;

        public EcsSystems(EcsWorld world)
        {
            _world = world;
            _initSystems = new Queue<IEcsInitSystem>();
            _runSystems = new List<IEcsRunSystem>();
            _removeOneFrameSystems = new List<IEcsRunSystem>();
            _services = new Dictionary<Type, object>();
        }

        public EcsSystems AddRunSystem(IEcsRunSystem runSystem)
        {
            _runSystems.Add(runSystem);
            return this;
        }

        public EcsSystems AddInitSystem(IEcsInitSystem initSystem)
        {
            _initSystems.Enqueue(initSystem);
            return this;
        }
    
        public EcsSystems AddService(object obj)
        {
            _services[obj.GetType()] = obj;
            return this;
        }

        public T GetService<T>()
        {
            var hasValue = _services.TryGetValue(typeof(T), out var res);
            if (hasValue)
                return (T) res;

            return (T) _services.Values.FirstOrDefault(val => val is T);
        }

        public void Init()
        {
            while (_initSystems.Any())
            {
                var system = _initSystems.Dequeue();
                system.Init(_world);
            }
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
}
