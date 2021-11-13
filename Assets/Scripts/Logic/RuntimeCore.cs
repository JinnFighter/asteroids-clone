using Ecs;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Components.Physics;
using Logic.Components.Time;
using Logic.Config;
using Logic.Containers;
using Logic.Events;
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

            var colliderFactoryContainer = new ColliderFactoryContainer();
            colliderFactoryContainer.AddColliderFactory<Ship>(new DefaultShipColliderFactory());
            colliderFactoryContainer.AddColliderFactory<Bullet>(new DefaultBulletColliderFactory());
            colliderFactoryContainer.AddColliderFactory<Asteroid>(new DefaultAsteroidColliderFactory());
            
            _systems
                .AddService(new GameFieldConfig(18, 10))
                .AddService(physicsConfiguration)
                .AddService(new AsteroidConfig(10f))
                .AddService(new CollisionsContainer())
                .AddService(new CollisionLayersContainer())
                .AddService(new ColliderFactoryContainer())
                .AddService(new PlayerInputEventHandlerContainer())
                .AddService(new ScoreEventHandlerContainer())
                .AddService(new ComponentEventHandlerContainer())
                .AddService(new ShipTransformEventHandlerContainer())
                .AddService(new ShipRigidBodyEventHandlerContainer())
                .AddService(new BulletTransformHandlerContainer())
                .AddService(new AsteroidTransformHandlerContainer())
                .AddService(new TargetTransformContainer())
                .AddService(new ScoreContainer())
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
            var disableOnGameOverTag = "DisableOnGameOver";

            var colliderFactoryContainer = _systems.GetService<ColliderFactoryContainer>();

            var targetTransformContainer = _systems.GetService<TargetTransformContainer>();
            _systems
                .AddInitSystem(new FillCollisionLayersSystem(collisionLayersContainer))
                .AddInitSystem(new CreatePlayerShipSystem(colliderFactoryContainer, collisionLayersContainer, 
                    _systems.GetService<ShipTransformEventHandlerContainer>(),
                    _systems.GetService<ShipRigidBodyEventHandlerContainer>(),
                    _systems.GetService<PlayerInputEventHandlerContainer>()))
                .AddInitSystem(new InitTargetTransformContainer(targetTransformContainer))
                .AddInitSystem(new CreateLaserSystem())
                .AddInitSystem(new CreateAsteroidCreatorSystem(randomizer))
                .AddInitSystem(new InitScoreSystem(_systems.GetService<ScoreContainer>(), 
                    _systems.GetService<ScoreEventHandlerContainer>()))
                .AddRunSystem(new ExecuteInputCommandsSystem(_systems.GetService<InputCommandQueue>()), disableOnGameOverTag)
                .AddRunSystem(new MoveShipsSystem())
                .AddRunSystem(new RotatePlayerShipSystem())
                .AddRunSystem(new CheckFireActionSystem())
                .AddRunSystem(new ShootLaserSystem())
                .AddRunSystem(new StartReloadingLaserSystem())
                .AddRunSystem(new UpdatePhysicsBodiesSystem(timeContainer,
                    _systems.GetService<PhysicsConfiguration>()), disableOnGameOverTag)
                .AddRunSystem(new RotatePhysicsBodiesSystem(), disableOnGameOverTag)
                .AddRunSystem(new WrapOffScreenObjectsSystem(gameFieldConfig), disableOnGameOverTag)
                .AddRunSystem(new CheckCollisionsSystem(collisionsContainer), disableOnGameOverTag)
                .AddRunSystem(new CheckShipCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckBulletCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckAsteroidCollisionsSystem(collisionsContainer))
                .AddRunSystem(new ClearCollisionsContainerSystem(collisionsContainer))
                .AddRunSystem(new UpdateTimersSystem(timeContainer), disableOnGameOverTag)
                .AddRunSystem(new FinishReloadingLaserSystem())
                .AddRunSystem(new DestroyBulletsSystem())
                .AddRunSystem(new DestroyAsteroidsSystem(asteroidConfig, randomizer))
                .AddRunSystem(new DestroyShipsSystem())
                .AddRunSystem(new DestroyPhysicsBodySystem())
                .AddRunSystem(new UpdateScoreSystem(_systems.GetService<ScoreContainer>()))
                .AddRunSystem(new CreateAsteroidEventSystem(gameFieldConfig, asteroidConfig, randomizer))
                .AddRunSystem(new SpawnAsteroidSystem(colliderFactoryContainer, collisionLayersContainer, 
                    _systems.GetService<ComponentEventHandlerContainer>(), 
                    _systems.GetService<AsteroidTransformHandlerContainer>()))
                .AddRunSystem(new SpawnBulletSystem(colliderFactoryContainer, collisionLayersContainer, 
                    _systems.GetService<BulletTransformHandlerContainer>()))
                .AddRunSystem(new GameOverSystem(_systems.GetService<ComponentEventHandlerContainer>()))
                .OneFrame<MovementInputAction>()
                .OneFrame<LookInputAction>()
                .OneFrame<FireInputAction>()
                .OneFrame<LaserFireInputAction>()
                .OneFrame<RotateEvent>()
                .OneFrame<UpdateScoreEvent>()
                .OneFrame<TimerEndEvent>()
                .OneFrame<CreateBulletEvent>()
                .OneFrame<CreateAsteroidEvent>()
                .OneFrame<CreateSaucerEvent>()
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
