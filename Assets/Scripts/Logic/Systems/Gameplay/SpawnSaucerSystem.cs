using Common;
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
    public class SpawnSaucerSystem : IEcsRunSystem
    {
        private readonly ColliderFactoryContainer _colliderFactoryContainer;
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly SaucerTransformHandlerContainer _saucerTransformHandlerContainer;

        public SpawnSaucerSystem(ColliderFactoryContainer colliderFactoryContainer,
            CollisionLayersContainer collisionLayersContainer,
            SaucerTransformHandlerContainer saucerTransformHandlerContainer)
        {
            _colliderFactoryContainer = colliderFactoryContainer;
            _collisionLayersContainer = collisionLayersContainer;
            _saucerTransformHandlerContainer = saucerTransformHandlerContainer;
        }

        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateSaucerEvent>();

            foreach (var index in filter)
            {
                var createSaucerEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                
                entity.AddComponent(new Saucer{ TargetTransform = createSaucerEvent.TargetTransform });

                var bodyTransform = new BodyTransform
                    { Position = createSaucerEvent.Position, Rotation = 0f, Direction = Vector2.Zero };
                _saucerTransformHandlerContainer.OnCreateEvent(bodyTransform);
                var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
                var colliderFactory = _colliderFactoryContainer.GetFactory<Saucer>();
                var collider = colliderFactory.CreateCollider(bodyTransform.Position);
                bodyTransform.PositionChangedEvent += collider.UpdatePosition;
                var targetCollisionLayers = collider.TargetCollisionLayers;
                targetCollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
                
                entity.AddComponent(new PhysicsBody
                {
                    Transform = bodyTransform,
                    RigidBody = rigidBody,
                    Collider = collider
                });

                entity.AddComponent(new Wrappable { IsWrappingX = false, IsWrappingY = false });
            }
        }
    }
}
