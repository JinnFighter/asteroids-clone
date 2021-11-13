using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Events;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly BulletFactory _bulletFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly BulletTransformHandlerContainer _bulletTransformHandlerContainer;

        public SpawnBulletSystem(BulletFactory bulletFactory, CollisionLayersContainer collisionLayersContainer, 
            BulletTransformHandlerContainer bulletTransformHandlerContainer)
        {
            _bulletFactory = bulletFactory;
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
