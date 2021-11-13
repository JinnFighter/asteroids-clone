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
    public class SpawnAsteroidSystem : IEcsRunSystem
    {
        private readonly AsteroidColliderFactory _asteroidColliderFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;
        private readonly AsteroidTransformHandlerContainer _asteroidTransformHandlerContainer;

        public SpawnAsteroidSystem(AsteroidColliderFactory asteroidColliderFactory, CollisionLayersContainer collisionLayersContainer,
            ComponentEventHandlerContainer componentComponentEventHandlerContainer, AsteroidTransformHandlerContainer asteroidTransformHandlerContainer)
        {
            _asteroidColliderFactory = asteroidColliderFactory;
            _collisionLayersContainer = collisionLayersContainer;
            _componentEventHandlerContainer = componentComponentEventHandlerContainer;
            _asteroidTransformHandlerContainer = asteroidTransformHandlerContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateAsteroidEvent>();

            foreach (var index in filter)
            {
                var createAsteroidEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Asteroid{ Stage = createAsteroidEvent.Stage });

                _componentEventHandlerContainer.HandleEvent(ref createAsteroidEvent);
                var velocity = createAsteroidEvent.Direction.Normalized * (createAsteroidEvent.Mass - 3 * createAsteroidEvent.Stage);
                var transform = new BodyTransform
                    { Position = createAsteroidEvent.Position, Rotation = 0f, Direction = velocity };
                _asteroidTransformHandlerContainer.OnCreateEvent(transform);
                var rigidBody = new PhysicsRigidBody { Mass = createAsteroidEvent.Mass, UseGravity = false };
                rigidBody.Velocity += velocity;
                var collider = _asteroidColliderFactory.CreateCollider(transform.Position);
                transform.PositionChangedEvent += collider.UpdatePosition;
                collider.CollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
                
                entity.AddComponent(new PhysicsBody
                {
                    Transform = transform,
                    RigidBody = rigidBody,
                    Collider = collider
                });
                
                entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });
            }
        }
    }
}
