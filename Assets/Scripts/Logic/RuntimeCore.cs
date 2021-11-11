using Ecs;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;
using Logic.Components.Time;
using Logic.Config;
using Logic.Conveyors;
using Logic.Factories;
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
            
            _systems
                .AddService(new GameFieldConfig(18, 10))
                .AddService(physicsConfiguration)
                .AddService(new AsteroidConfig(10f))
                .AddService(new CollisionsContainer())
                .AddService(new CollisionLayersContainer())
                .AddService(new ShipConveyor())
                .AddService<AsteroidFactory>(new DefaultAsteroidFactory())
                .AddService<BulletFactory>(new DefaultBulletFactory())
                .AddService(timeContainer)
                .AddService<IDeltaTimeCounter>(new DefaultDeltaTimeCounter())
                .AddService(new InputCommandQueue())
                .AddService<IRandomizer>(new Randomizer());
        }

        public void Init()
        {
            var timeContainer = _systems.GetService<TimeContainer>();
            var gameFieldConfig = _systems.GetService<GameFieldConfig>();
            var asteroidConfig = _systems.GetService<AsteroidConfig>();

            var collisionsContainer = _systems.GetService<CollisionsContainer>();
            var collisionLayersContainer = _systems.GetService<CollisionLayersContainer>();

            var randomizer = _systems.GetService<IRandomizer>();
            
            _systems
                .AddInitSystem(new FillCollisionLayersSystem(collisionLayersContainer))
                .AddInitSystem(new CreatePlayerShipSystem(_systems.GetService<ShipConveyor>()))
                .AddInitSystem(new CreateLaserSystem())
                .AddInitSystem(new CreateAsteroidCreatorSystem(randomizer))
                .AddRunSystem(new ExecuteInputCommandsSystem(_systems.GetService<InputCommandQueue>()))
                .AddRunSystem(new MoveShipsSystem())
                .AddRunSystem(new RotatePlayerShipSystem())
                .AddRunSystem(new CheckFireActionSystem())
                .AddRunSystem(new ShootLaserSystem())
                .AddRunSystem(new StartReloadingLaserSystem())
                .AddRunSystem(new UpdatePhysicsBodiesSystem(timeContainer,
                    _systems.GetService<PhysicsConfiguration>()))
                .AddRunSystem(new RotatePhysicsBodiesSystem())
                .AddRunSystem(new WrapOffScreenObjectsSystem(gameFieldConfig))
                .AddRunSystem(new CheckCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckShipCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckBulletCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckAsteroidCollisionsSystem(collisionsContainer))
                .AddRunSystem(new ClearCollisionsContainerSystem(collisionsContainer))
                .AddRunSystem(new UpdateTimersSystem(timeContainer))
                .AddRunSystem(new FinishReloadingLaserSystem())
                .AddRunSystem(new DestroyBulletsSystem())
                .AddRunSystem(new DestroyAsteroidsSystem(asteroidConfig, randomizer))
                .AddRunSystem(new DestroyShipsSystem())
                .AddRunSystem(new DestroyPhysicsBodySystem())
                .AddRunSystem(new CreateAsteroidEventSystem(gameFieldConfig, asteroidConfig, randomizer))
                .AddRunSystem(new SpawnAsteroidSystem(_systems.GetService<AsteroidFactory>(), collisionLayersContainer))
                .AddRunSystem(new SpawnBulletSystem(_systems.GetService<BulletFactory>(), collisionLayersContainer))
                .OneFrame<MovementInputAction>()
                .OneFrame<LookInputAction>()
                .OneFrame<FireInputAction>()
                .OneFrame<LaserFireInputAction>()
                .OneFrame<RotateEvent>()
                .OneFrame<TimerEndEvent>()
                .OneFrame<CreateBulletEvent>()
                .OneFrame<CreateAsteroidEvent>()
                .OneFrame<ShootLaserEvent>()
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
