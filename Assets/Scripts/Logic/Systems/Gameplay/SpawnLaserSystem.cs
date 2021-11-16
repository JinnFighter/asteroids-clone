using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Components.Time;
using Logic.Events;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnLaserSystem : IEcsRunSystem
    {
        private readonly LaserTransformHandlerContainer _laserTransformHandlerContainer;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public SpawnLaserSystem(LaserTransformHandlerContainer laserTransformHandlerContainer,
            CollisionLayersContainer collisionLayersContainer)
        {
            _laserTransformHandlerContainer = laserTransformHandlerContainer;
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
                    { Position = laserEvent.Position, Rotation = laserEvent.Rotation, Direction = laserEvent.Direction };
                _laserTransformHandlerContainer.OnCreateEvent(bodyTransform);
                var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
                var collider = new RayPhysicsCollider(laserEvent.Position, laserEvent.Direction);
                bodyTransform.PositionChangedEvent += collider.UpdatePosition;
                var targetCollisionLayers = collider.TargetCollisionLayers;
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("saucers"));

                entity.AddComponent(new PhysicsBody
                {
                    Transform = bodyTransform,
                    RigidBody = rigidBody,
                    Collider = collider
                });
                
                entity.AddComponent(new Timer{ GameplayTimer = new GameplayTimer{ StartTime = 0.3f, CurrentTime = 0.3f } });
                entity.AddComponent(new Counting());
            }
        }
    }
}
