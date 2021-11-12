using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly BulletFactory _bulletFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public SpawnBulletSystem(BulletFactory bulletFactory, CollisionLayersContainer collisionLayersContainer)
        {
            _bulletFactory = bulletFactory;
            _collisionLayersContainer = collisionLayersContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateBulletEvent>();

            foreach (var index in filter)
            {
                var createBulletEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();

                entity.AddComponent(new Bullet());

                var bodyTransform =
                    _bulletFactory.CreateTransform(createBulletEvent.Position, 0f, createBulletEvent.Direction);
                var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
                rigidBody.Velocity += createBulletEvent.Velocity;
                var collider = _bulletFactory.CreateCollider(bodyTransform.Position);
                bodyTransform.PositionChangedEvent += collider.UpdatePosition;
                var targetCollisionLayers = collider.TargetCollisionLayers;
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                
                
                entity.AddComponent(new PhysicsBody
                {
                    Transform = bodyTransform,
                    RigidBody = rigidBody,
                    Collider = collider
                });
            
                entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });
            }
        }
    }
}
