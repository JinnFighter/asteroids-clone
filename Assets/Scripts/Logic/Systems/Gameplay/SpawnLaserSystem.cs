using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnLaserSystem : IEcsRunSystem
    {
        private readonly CollisionLayersConfig _collisionLayersConfig;
        private readonly IPhysicsBodyBuilder _physicsBodyBuilder;
        private readonly LaserConfig _laserConfig;

        public SpawnLaserSystem(CollisionLayersConfig collisionLayersConfig, IPhysicsBodyBuilder physicsBodyBuilder, 
            LaserConfig laserConfig)
        {
            _collisionLayersConfig = collisionLayersConfig;
            _physicsBodyBuilder = physicsBodyBuilder;
            _laserConfig = laserConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateLaserEvent>();

            foreach (var index in filter)
            {
                var laserEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Laser());
                
                _physicsBodyBuilder.Reset();
                var transform = new TransformBody
                    { Position = laserEvent.Position, Rotation = laserEvent.Rotation, Direction = laserEvent.Direction };
                _physicsBodyBuilder.AddTransform<Laser>(transform);
                _physicsBodyBuilder.AddRigidBody<Laser>(new PhysicsRigidBody { Mass = 1f, UseGravity = false });
                _physicsBodyBuilder.AddCollider(new RayPhysicsCollider(laserEvent.Position, laserEvent.Direction));
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.AsteroidsLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.SaucersLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.BulletsLayer);

                entity.AddComponent(_physicsBodyBuilder.GetResult());
                
                entity.AddComponent(new Timer{ GameplayTimer = new GameplayTimer{ StartTime = _laserConfig.LaserLifeTime, 
                    CurrentTime = _laserConfig.LaserLifeTime } });
                entity.AddComponent(new Counting());
            }
        }
    }
}
