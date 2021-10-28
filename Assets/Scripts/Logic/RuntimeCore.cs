using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Time;
using Logic.Config;
using Logic.Conveyors;
using Logic.EventAttachers;
using Logic.Services;
using Logic.Systems.Gameplay;
using Logic.Systems.Physics;
using Logic.Systems.Time;
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
            var timeContainer = new TimeContainer();
            var physicsConfiguration = new PhysicsConfiguration();
            var asteroidConveyor = new AsteroidConveyor();
            asteroidConveyor.AddNextConveyor(new AsteroidPhysicsBodyConveyor());
            _systems
                .AddService(new GameFieldConfig(1080, 520))
                .AddService(physicsConfiguration)
                .AddService(new ShipConveyor())
                .AddService(asteroidConveyor)
                .AddService<IEventAttacher, DefaultEventAttacher>(new DefaultEventAttacher(_world))
                .AddService(timeContainer)
                .AddService<IDeltaTimeCounter, DefaultDeltaTimeCounter>(new DefaultDeltaTimeCounter())
                .AddService(new InputCommandQueue());
        }

        public void Init()
        {
            var timeContainer = _systems.GetService<TimeContainer>();
            
            _systems
                .AddInitSystem(new CreatePlayerShipSystem(_systems.GetService<ShipConveyor>()))
                .AddInitSystem(new CreateAsteroidCreatorSystem())
                .AddRunSystem(new ExecuteInputCommandsSystem(_systems.GetService<InputCommandQueue>()))
                .AddRunSystem(new MoveShipsSystem())
                .AddRunSystem(new UpdatePhysicsBodiesSystem(timeContainer,
                    _systems.GetService<PhysicsConfiguration>()))
                .AddRunSystem(new UpdateTimersSystem(timeContainer))
                .AddRunSystem(new CreateAsteroidEventSystem())
                .AddRunSystem(new SpawnAsteroidSystem(_systems.GetService<AsteroidConveyor>()))
                .OneFrame<InputAction>()
                .OneFrame<MovementInputAction>()
                .OneFrame<LookInputAction>()
                .OneFrame<FireInputAction>()
                .OneFrame<TimerEndEvent>()
                .OneFrame<CreateAsteroidEvent>()
                .Init(_world);
        }

        public void Run()
        {
            var timeContainer = _systems.GetService<TimeContainer>();
            var deltaTimeCounter = _systems.GetService<IDeltaTimeCounter>();
            deltaTimeCounter.Reset();
            
            _systems.Run(_world);
            _world.RemoveEmptyEntities();
            
            timeContainer.DeltaTime = deltaTimeCounter.GetDeltaTime();
        }

        public void AddService<T>(in T service) => _systems.AddService(service);
        
        public void AddService<T, T1>(in T1 service) where T1 : T => _systems.AddService<T>(service);

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            _systems.Destroy(_world);
            _world.Destroy();
        }
    }
}
