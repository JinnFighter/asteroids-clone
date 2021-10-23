using Ecs;
using Logic.Components.Input;
using Logic.Conveyors;
using Logic.EventAttachers;
using Logic.Factories;
using Logic.Services;
using Logic.Systems.Gameplay;
using Logic.Systems.Physics;
using Physics;

namespace Logic
{
    public class RuntimeCore
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        public RuntimeCore()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems();
        }

        public void Setup()
        {
            var physicsWorld = new PhysicsWorld();
            var timeContainer = new TimeContainer();
            _systems
                .AddService(physicsWorld)
                .AddService(new ShipConveyor())
                .AddService<IEventAttacher, DefaultEventAttacher>(new DefaultEventAttacher(_world))
                .AddService(timeContainer)
                .AddService<IDeltaTimeCounter, DefaultDeltaTimeCounter>(new DefaultDeltaTimeCounter());
        }

        public void Init() => _systems
            .AddInitSystem(new CreatePlayerShipSystem(_systems.GetService<ShipConveyor>()))
            .AddRunSystem(new UpdatePhysicsBodiesSystem(_systems.GetService<TimeContainer>()))
            .OneFrame<InputAction>()
            .Init(_world);

        public void Run()
        {
            var timeContainer = _systems.GetService<TimeContainer>();
            var deltaTimeCounter = _systems.GetService<IDeltaTimeCounter>();
            deltaTimeCounter.Reset();
            
            _systems.Run(_world);
            _world.RemoveEmptyEntities();
            
            timeContainer.DeltaTime = deltaTimeCounter.GetDeltaTime();
        }

        public void AddService(object service) => _systems.AddService(service);
        
        public void AddService<T, T1>(T1 service) where T1 : T => _systems.AddService<T, T1>(service);

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            _systems.Destroy(_world);
            _world.Destroy();
        }
    }
}
