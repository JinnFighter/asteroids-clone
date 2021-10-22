using System;
using System.Collections.Generic;
using System.Linq;
using Ecs.Interfaces;
using Ecs.Systems;

namespace Ecs
{
    public class EcsSystems
    {
        private readonly Queue<IEcsInitSystem> _initSystems;
        private readonly List<IEcsRunSystem> _runSystems;
        private readonly Queue<IEcsOnDestroySystem> _onDestroySystems;
        private readonly List<IEcsRunSystem> _removeOneFrameSystems;
        
        private readonly Dictionary<Type, object> _services;

        public EcsSystems()
        {
            _initSystems = new Queue<IEcsInitSystem>();
            _runSystems = new List<IEcsRunSystem>();
            _onDestroySystems = new Queue<IEcsOnDestroySystem>();
            _removeOneFrameSystems = new List<IEcsRunSystem>();
            _services = new Dictionary<Type, object>();
        }
        
        public EcsSystems AddInitSystem(IEcsInitSystem initSystem)
        {
            _initSystems.Enqueue(initSystem);
            return this;
        }

        public EcsSystems AddRunSystem(IEcsRunSystem runSystem)
        {
            _runSystems.Add(runSystem);
            return this;
        }

        public EcsSystems AddOnDestroySystem(IEcsOnDestroySystem onDestroySystem)
        {
            _onDestroySystems.Enqueue(onDestroySystem);
            return this;
        }
    
        public EcsSystems AddService(object obj)
        {
            _services[obj.GetType()] = obj;
            return this;
        }
        
        public EcsSystems AddService<T, T1>(T1 obj) where T1 : T
        {
            _services[typeof(T)] = obj;
            return this;
        }

        public T GetService<T>()
        {
            var hasValue = _services.TryGetValue(typeof(T), out var res);
            if (hasValue)
                return (T) res;

            return (T) _services.Values.FirstOrDefault(val => val is T);
        }

        public void Init(EcsWorld world)
        {
            while (_initSystems.Any())
            {
                var system = _initSystems.Dequeue();
                system.Init(world);
            }
        }

        public void Run(EcsWorld world)
        {
            foreach (var system in _runSystems)
            {
                system.Run(world);
                world.UpdateFilters();
            }

            foreach (var removeOneFrameSystem in _removeOneFrameSystems)
                removeOneFrameSystem.Run(world);
            world.UpdateFilters();
        }

        public EcsSystems OneFrame<T>() where T : struct
        {
            _removeOneFrameSystems.Add(new RemoveOneFrameSystem<T>());
            return this;
        }

        public void Destroy(EcsWorld world)
        {
            while (_onDestroySystems.Any())
            {
                var system = _onDestroySystems.Dequeue();
                system.OnDestroy(world);
            }
            
            _runSystems.Clear();
            _removeOneFrameSystems.Clear();
            
            _services.Clear();
        }
    }
}
