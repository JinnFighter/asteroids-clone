using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Containers;
using Logic.Events;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly ColliderFactoryContainer _colliderFactoryContainer;
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly BulletTransformHandlerContainer _bulletTransformHandlerContainer;

        public SpawnBulletSystem(ColliderFactoryContainer colliderFactoryContainer, CollisionLayersContainer collisionLayersContainer, 
            BulletTransformHandlerContainer bulletTransformHandlerContainer)
        {
            _colliderFactoryContainer = colliderFactoryContainer;
            _collisionLayersContainer = collisionLayersContainer;
            _bulletTransformHandlerContainer = bulletTransformHandlerContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateBulletEvent>();

            foreach (var index in filter)
            {
                var createBulletEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();

                entity.AddComponent(new Bullet());

                var bodyTransform = new BodyTransform
                    { Position = createBulletEvent.Position, Rotation = 0f, Direction = createBulletEvent.Direction };
                _bulletTransformHandlerContainer.OnCreateEvent(bodyTransform);
                var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
                rigidBody.Velocity += createBulletEvent.Velocity;
                var colliderFactory = _colliderFactoryContainer.GetFactory<Bullet>();
                var collider = colliderFactory.CreateCollider(bodyTransform.Position);
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
