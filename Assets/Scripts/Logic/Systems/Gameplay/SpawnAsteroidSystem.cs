using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnAsteroidSystem : IEcsRunSystem
    {
        private readonly AsteroidFactory _asteroidFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public SpawnAsteroidSystem(AsteroidFactory asteroidFactory, CollisionLayersContainer collisionLayersContainer)
        {
            _asteroidFactory = asteroidFactory;
            _collisionLayersContainer = collisionLayersContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateAsteroidEvent>();

            foreach (var index in filter)
            {
                var createAsteroidEvent = filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Asteroid{ Stage = createAsteroidEvent.Stage });

                _asteroidFactory.SetStage(createAsteroidEvent.Stage);
                var velocity = createAsteroidEvent.Direction.Normalized * (createAsteroidEvent.Mass - 3 * createAsteroidEvent.Stage);
                var transform =
                    _asteroidFactory.CreateTransform(createAsteroidEvent.Position, 0f, velocity);
                var rigidBody = _asteroidFactory.CreateRigidBody(createAsteroidEvent.Mass, false);
                rigidBody.Velocity += velocity;
                var collider = _asteroidFactory.CreateCollider(transform.Position);
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
