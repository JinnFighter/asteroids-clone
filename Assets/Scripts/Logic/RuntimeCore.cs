using Common;
using Ecs;
using Ecs.Interfaces;
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
            colliderFactoryContainer.AddColliderFactory<Saucer>(new DefaultShipColliderFactory());
            colliderFactoryContainer.AddColliderFactory<Laser>(new DefaultLaserColliderFactory());

            var gameFieldConfig = new GameFieldConfig(18, 10);
            _systems
                .AddService(gameFieldConfig)
                .AddService(physicsConfiguration)
                .AddService(new ShipConfig{ GunPositionOffset = 0.5f })
                .AddService(new AsteroidConfig{ DefaultMass = 10f, MinRespawnTime = 3, MaxRespawnTime = 5, 
                    MinAngle = 15, MaxAngle = 165, MinStage = 1, MaxStage = 4, VelocityCoefficient = 3f })
                .AddService(new SaucerConfig())
                .AddService(new LaserConfig{ ReloadTime = 7f, LaserLifeTime = 0.2f })
                .AddService(new BulletConfig{ LifeTime = 4f })
                .AddService(new ScoreConfig{ AsteroidScore = 10, SaucerScore = 50 })
                .AddService(new CollisionLayersConfig{ AsteroidsLayer = "Asteroids", ShipsLayer = "Ships", 
                    SaucersLayer = "Saucers", BulletsLayer = "Bullets" })
                .AddService(new CollisionsContainer())
                .AddService(new CollisionLayersContainer())
                .AddService(new ColliderFactoryContainer())
                .AddService(new QuadTree(0, new Rectangle(Vector2.Zero, gameFieldConfig.Width,
                    gameFieldConfig.Height)))
                .AddService(new TransformHandlerKeeper())
                .AddService(new RigidBodyHandlerKeeper())
                .AddService(new PlayerInputHandlerKeeper())
                .AddService(new ScoreEventHandlerContainer())
                .AddService(new ComponentEventHandlerContainer())
                .AddService(new AmmoMagazineHandlerKeeper())
                .AddService(new TimerHandlerKeeper())
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

            var shipConfig = _systems.GetService<ShipConfig>();
            var saucerConfig = _systems.GetService<SaucerConfig>();
            var laserConfig = _systems.GetService<LaserConfig>();
            var bulletConfig = _systems.GetService<BulletConfig>();
            var scoreConfig = _systems.GetService<ScoreConfig>();
            var collisionLayersConfig = _systems.GetService<CollisionLayersConfig>();
            
            var transformHandlerKeeper = _systems.GetService<TransformHandlerKeeper>();
            var rigidBodyHandlerKeeper = _systems.GetService<RigidBodyHandlerKeeper>();
            var timerHandlerKeeper = _systems.GetService<TimerHandlerKeeper>();
            var ammoMagazineHandlerKeeper = _systems.GetService<AmmoMagazineHandlerKeeper>();

            var physicsBodyBuilder =
                new PhysicsBodyBuilder(transformHandlerKeeper, rigidBodyHandlerKeeper, collisionLayersContainer);

            var quadTree = _systems.GetService<QuadTree>();
            
            _systems
                .AddInitSystem(new FillCollisionLayersSystem(collisionLayersContainer, collisionLayersConfig))
                .AddInitSystem(new CreatePlayerShipSystem(collisionLayersConfig, colliderFactoryContainer, physicsBodyBuilder))
                .AddInitSystem(new CreatePlayerInputReceiverSystem(_systems.GetService<PlayerInputHandlerKeeper>()))
                .AddInitSystem(new InitTargetTransformContainer(targetTransformContainer))
                .AddInitSystem(new CreateLaserGunSystem(laserConfig, ammoMagazineHandlerKeeper, timerHandlerKeeper))
                .AddInitSystem(new CreateAsteroidSpawnerSystem(randomizer, asteroidConfig))
                .AddInitSystem(new InitSaucerSpawnerSystem(saucerConfig, randomizer))
                .AddInitSystem(new InitScoreSystem(_systems.GetService<ScoreContainer>(), 
                    _systems.GetService<ScoreEventHandlerContainer>()))
                .AddRunSystem(new ExecuteInputCommandsSystem(_systems.GetService<InputCommandQueue>()), disableOnGameOverTag)
                .AddRunSystem(new MoveShipsSystem())
                .AddRunSystem(new RotatePlayerShipSystem())
                .AddRunSystem(new CheckSaucerDirectionSystem())
                .AddRunSystem(new CheckBulletFireActionSystem(shipConfig))
                .AddRunSystem(new ShootLaserSystem(shipConfig))
                .AddRunSystem(new StartReloadingLaserSystem(laserConfig))
                .AddRunSystem(new UpdatePhysicsBodiesSystem(timeContainer,
                    _systems.GetService<PhysicsConfiguration>()), disableOnGameOverTag)
                .AddRunSystem(new RotatePhysicsBodiesSystem(), disableOnGameOverTag)
                .AddRunSystem(new WrapOffScreenObjectsSystem(gameFieldConfig), disableOnGameOverTag)
                .AddRunSystem(new FillQuadTreeSystem(quadTree), disableOnGameOverTag)
                .AddRunSystem(new CheckCollisionsSystem(collisionsContainer, quadTree), disableOnGameOverTag)
                .AddRunSystem(new CheckShipCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckBulletCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckAsteroidCollisionsSystem(collisionsContainer))
                .AddRunSystem(new CheckSaucerCollisionsSystem(collisionsContainer))
                .AddRunSystem(new ClearCollisionsContainerSystem(collisionsContainer))
                .AddRunSystem(new UpdateTimersSystem(timeContainer), disableOnGameOverTag)
                .AddRunSystem(new FinishReloadingLaserSystem())
                .AddRunSystem(new CreateDestroyLaserEventSystem())
                .AddRunSystem(new CreateDestroyBulletEventSystem())
                .AddRunSystem(new DestroyBulletsSystem())
                .AddRunSystem(new DestroyLaserSystem())
                .AddRunSystem(new DestroyAsteroidsSystem(scoreConfig, asteroidConfig, randomizer))
                .AddRunSystem(new DestroyShipsSystem())
                .AddRunSystem(new DestroySaucerSystem(scoreConfig))
                .AddRunSystem(new DestroyPhysicsBodySystem())
                .AddRunSystem(new DestroyTimersSystem())
                .AddRunSystem(new UpdateScoreSystem(_systems.GetService<ScoreContainer>()))
                .AddRunSystem(new CreateAsteroidEventSystem(gameFieldConfig, asteroidConfig, randomizer))
                .AddRunSystem(new CreateSpawnSaucerEventSystem(gameFieldConfig, randomizer, targetTransformContainer))
                .AddRunSystem(new SpawnSaucerSystem(collisionLayersConfig, physicsBodyBuilder, 
                    colliderFactoryContainer.GetFactory<Saucer>()))
                .AddRunSystem(new SpawnAsteroidSystem(collisionLayersConfig, colliderFactoryContainer.GetFactory<Asteroid>(),
                    _systems.GetService<ComponentEventHandlerContainer>(), physicsBodyBuilder,
                    asteroidConfig))
                .AddRunSystem(new SpawnBulletSystem(collisionLayersConfig, physicsBodyBuilder, colliderFactoryContainer.GetFactory<Bullet>(), bulletConfig))
                .AddRunSystem(new SpawnLaserSystem(collisionLayersConfig, physicsBodyBuilder, laserConfig))
                .AddRunSystem(new GameOverSystem(_systems.GetService<ComponentEventHandlerContainer>()))
                .OneFrame<LookInputAction>()
                .OneFrame<FireInputAction>()
                .OneFrame<LaserFireInputAction>()
                .OneFrame<RotateEvent>()
                .OneFrame<UpdateScoreEvent>()
                .OneFrame<TimerEndEvent>()
                .OneFrame<CreateBulletEvent>()
                .OneFrame<CreateAsteroidEvent>()
                .OneFrame<CreateSaucerEvent>()
                .OneFrame<CreateLaserEvent>()
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

            timeContainer.DeltaTime = deltaTimeCounter.GetDeltaTime();
        }

        public RuntimeCore AddInitSystem(IEcsInitSystem initSystem)
        {
            _systems.AddInitSystem(initSystem);
            return this;
        }

        public RuntimeCore AddService<T>(in T service)
        {
            _systems.AddService(service);
            return this;
        }

        public T GetService<T>() => _systems.GetService<T>();

        public void Destroy()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}
