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
        private readonly List<RunSystemContainer> _runSystemContainers;
        private readonly Queue<IEcsOnDestroySystem> _onDestroySystems;
        private readonly List<IEcsRunSystem> _removeOneFrameSystems;
        
        private readonly Dictionary<Type, object> _services;

        private readonly string _internalTag;

        public EcsSystems()
        {
            _initSystems = new Queue<IEcsInitSystem>();
            _runSystemContainers = new List<RunSystemContainer>();
            _onDestroySystems = new Queue<IEcsOnDestroySystem>();
            _removeOneFrameSystems = new List<IEcsRunSystem>();
            _services = new Dictionary<Type, object>();
            _internalTag = "Internal";
        }
        
        public EcsSystems AddInitSystem(IEcsInitSystem initSystem)
        {
            _initSystems.Enqueue(initSystem);
            return this;
        }

        public EcsSystems AddRunSystem(IEcsRunSystem runSystem, string tag = "")
        {
            _runSystemContainers.Add(new RunSystemContainer(runSystem, tag : tag));
            return this;
        }

        public EcsSystems AddOnDestroySystem(IEcsOnDestroySystem onDestroySystem)
        {
            _onDestroySystems.Enqueue(onDestroySystem);
            return this;
        }

        public EcsSystems AddService<T>(in T obj) => UpdateServicesContainer(obj);

        private EcsSystems UpdateServicesContainer<T>(in T obj)
        {
            _services[typeof(T)] = obj;
            return this;
        }

        public void SetRunSystemState(string tag, bool state)
        {
            foreach (var container in _runSystemContainers.Where(container => container.Tag != _internalTag && container.Tag == tag))
                container.IsActive = state;
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
            foreach (var runSystemContainer in _runSystemContainers.Where(runSystemContainer => runSystemContainer.IsActive))
            {
                var system = runSystemContainer.System;
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
            
            _runSystemContainers.Clear();
            _removeOneFrameSystems.Clear();
            
            _services.Clear();
        }
    }
}
