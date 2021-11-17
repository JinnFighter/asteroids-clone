using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Events;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class SpawnAsteroidSystem : IEcsRunSystem
    {
        private readonly IColliderFactory _colliderFactory;
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;
        private readonly IPhysicsBodyBuilder _physicsBodyBuilder;

        public SpawnAsteroidSystem(IColliderFactory colliderFactory,
            ComponentEventHandlerContainer componentComponentEventHandlerContainer, IPhysicsBodyBuilder physicsBodyBuilder)
        {
            _colliderFactory = colliderFactory;
            _componentEventHandlerContainer = componentComponentEventHandlerContainer;
            _physicsBodyBuilder = physicsBodyBuilder;
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
                
                _physicsBodyBuilder.Reset();
                var position = createAsteroidEvent.Position;
                var velocity = createAsteroidEvent.Direction.Normalized * (createAsteroidEvent.Mass - 3 * createAsteroidEvent.Stage);
                _physicsBodyBuilder.AddTransform<Asteroid>(new TransformBody
                    { Position = position, Rotation = 0f, Direction = velocity });
                _physicsBodyBuilder.AddRigidBody<Asteroid>(new PhysicsRigidBody { Mass = createAsteroidEvent.Mass, 
                    Velocity = velocity, UseGravity = false });
                _physicsBodyBuilder.AddCollider(_colliderFactory.CreateCollider(position));
                _physicsBodyBuilder.AddCollisionLayer("asteroids");

                entity.AddComponent(_physicsBodyBuilder.GetResult());
                
                entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });
            }
        }
    }
}
