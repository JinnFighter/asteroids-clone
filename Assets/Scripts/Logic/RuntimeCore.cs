using Ecs;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;
using Logic.Components.Time;
using Logic.Config;
using Logic.Conveyors;
using Logic.Services;
using Logic.Systems.GameField;
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
                .AddService(new GameFieldConfig(18, 10))
                .AddService(physicsConfiguration)
                .AddService(new CollisionsContainer())
                .AddService(new CollisionLayersContainer())
                .AddService(new ShipConveyor())
                .AddService(asteroidConveyor)
                .AddService(timeContainer)
                .AddService<IDeltaTimeCounter>(new DefaultDeltaTimeCounter())
                .AddService(new InputCommandQueue());
        }

        public void Init()
        {
            var timeContainer = _systems.GetService<TimeContainer>();
            var gameFieldConfig = _systems.GetService<GameFieldConfig>();

            var collisionsContainer = _systems.GetService<CollisionsContainer>();
            var collisionLayersContainer = _systems.GetService<CollisionLayersContainer>();
            _systems
                .AddInitSystem(new FillCollisionLayersSystem(collisionLayersContainer))
                .AddInitSystem(new CreatePlayerShipSystem(_systems.GetService<ShipConveyor>()))
                .AddInitSystem(new CreateAsteroidCreatorSystem())
                .AddRunSystem(new ExecuteInputCommandsSystem(_systems.GetService<InputCommandQueue>()))
                .AddRunSystem(new MoveShipsSystem())
                .AddRunSystem(new RotatePlayerShipSystem())
                .AddRunSystem(new UpdatePhysicsBodiesSystem(timeContainer,
                    _systems.GetService<PhysicsConfiguration>()))
                .AddRunSystem(new RotatePhysicsBodiesSystem())
                .AddRunSystem(new CheckCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckShipCollisionsSystem(collisionsContainer))
                .AddRunSystem(new ClearCollisionsContainerSystem(collisionsContainer))
                .AddRunSystem(new WrapOffScreenObjectsSystem(gameFieldConfig))
                .AddRunSystem(new UpdateTimersSystem(timeContainer))
                .AddRunSystem(new DestroyAsteroidsSystem())
                .AddRunSystem(new DestroyShipsSystem())
                .AddRunSystem(new DestroyPhysicsBodySystem())
                .AddRunSystem(new CreateAsteroidEventSystem(gameFieldConfig))
                .AddRunSystem(new SpawnAsteroidSystem(_systems.GetService<AsteroidConveyor>()))
                .OneFrame<MovementInputAction>()
                .OneFrame<LookInputAction>()
                .OneFrame<FireInputAction>()
                .OneFrame<RotateEvent>()
                .OneFrame<TimerEndEvent>()
                .OneFrame<CreateBulletEvent>()
                .OneFrame<CreateAsteroidEvent>()
                .OneFrame<GameOverEvent>()
                .OneFrame<DestroyEvent>()
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

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            _systems.Destroy(_world);
            _world.Destroy();
        }
    }
}
