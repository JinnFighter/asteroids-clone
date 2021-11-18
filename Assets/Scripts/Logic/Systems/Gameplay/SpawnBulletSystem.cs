using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly CollisionLayersConfig _collisionLayersConfig;
        private readonly IPhysicsBodyBuilder _physicsBodyBuilder;
        private readonly IColliderFactory _colliderFactory;
        private readonly BulletConfig _bulletConfig;

        public SpawnBulletSystem(CollisionLayersConfig collisionLayersConfig, IPhysicsBodyBuilder physicsBodyBuilder, IColliderFactory colliderFactory, BulletConfig bulletConfig)
        {
            _collisionLayersConfig = collisionLayersConfig;
            _physicsBodyBuilder = physicsBodyBuilder;
            _colliderFactory = colliderFactory;
            _bulletConfig = bulletConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateBulletEvent>();

            foreach (var index in filter)
            {
                var createBulletEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();

                entity.AddComponent(new Bullet());

                var position = createBulletEvent.Position;
                _physicsBodyBuilder.Reset();
                _physicsBodyBuilder.AddTransform<Bullet>(new TransformBody
                    { Position = position, Rotation = 0f, Direction = createBulletEvent.Direction });
                
                _physicsBodyBuilder.AddRigidBody<Bullet>(new PhysicsRigidBody { Mass = 1f, Velocity = createBulletEvent.Velocity, UseGravity = false });
                _physicsBodyBuilder.AddCollider(_colliderFactory.CreateCollider(position));
                _physicsBodyBuilder.AddCollisionLayer(_collisionLayersConfig.BulletsLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.AsteroidsLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.ShipsLayer);
                _physicsBodyBuilder.AddTargetCollisionLayer(_collisionLayersConfig.SaucersLayer);

                entity.AddComponent(_physicsBodyBuilder.GetResult());
            
                entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });
                entity.AddComponent(new Timer{ GameplayTimer = new GameplayTimer{ StartTime = _bulletConfig.LifeTime, 
                    CurrentTime = _bulletConfig.LifeTime }});
                entity.AddComponent(new Counting());
            }
        }
    }
}
