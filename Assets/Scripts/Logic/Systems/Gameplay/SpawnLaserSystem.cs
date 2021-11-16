using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Components.Time;
using Logic.Containers;
using Logic.Events;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnLaserSystem : IEcsRunSystem
    {
        private readonly LaserTransformHandlerContainer _laserTransformHandlerContainer;
        private readonly ColliderFactoryContainer _colliderFactoryContainer;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public SpawnLaserSystem(LaserTransformHandlerContainer laserTransformHandlerContainer,
            ColliderFactoryContainer colliderFactoryContainer,
            CollisionLayersContainer collisionLayersContainer)
        {
            _laserTransformHandlerContainer = laserTransformHandlerContainer;
            _colliderFactoryContainer = colliderFactoryContainer;
            _collisionLayersContainer = collisionLayersContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateLaserEvent>();

            foreach (var index in filter)
            {
                var laserEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Laser());
                var bodyTransform = new BodyTransform
                    { Position = laserEvent.Position, Rotation = 0f, Direction = laserEvent.Direction };
                _laserTransformHandlerContainer.OnCreateEvent(bodyTransform);
                var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
                var colliderFactory = _colliderFactoryContainer.GetFactory<Laser>();
                var collider = colliderFactory.CreateCollider(bodyTransform.Position);
                bodyTransform.PositionChangedEvent += collider.UpdatePosition;
                var targetCollisionLayers = collider.TargetCollisionLayers;
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("saucers"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("bullets"));
                
                entity.AddComponent(new PhysicsBody
                {
                    Transform = bodyTransform,
                    RigidBody = rigidBody,
                    Collider = collider
                });
                
                entity.AddComponent(new Timer{GameplayTimer = new GameplayTimer{ StartTime = 0.5f, CurrentTime = 0.5f} });
                entity.AddComponent(new Counting());
            }
        }
    }
}
