using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Containers;
using Logic.Events;
using Logic.Factories;
using Logic.Input;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreatePlayerShipSystem : IEcsInitSystem
    {
        private readonly PlayerInputHandlerKeeper _playerInputHandlerKeeper;
        private readonly ColliderFactoryContainer _colliderFactoryContainer;
        private readonly IPhysicsBodyBuilder _physicsBodyBuilder;

        public CreatePlayerShipSystem(ColliderFactoryContainer colliderFactoryContainer, IPhysicsBodyBuilder physicsBodyBuilder,
            PlayerInputHandlerKeeper playerInputHandlerKeeper)
        {
            _colliderFactoryContainer = colliderFactoryContainer;
            _physicsBodyBuilder = physicsBodyBuilder;
            _playerInputHandlerKeeper = playerInputHandlerKeeper;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            
            entity.AddComponent(new Ship{ Speed = 2f });

            _physicsBodyBuilder.Reset();
            var transform = new BodyTransform { Position = Vector2.Zero, Rotation = 0f, Direction = new Vector2(0, 1) };
            _physicsBodyBuilder.AddTransform<Ship>(transform);
            _physicsBodyBuilder.AddRigidBody<Ship>(new PhysicsRigidBody { Mass = 1f, UseGravity = false });
            var colliderFactory = _colliderFactoryContainer.GetFactory<Ship>();
            var collider = colliderFactory.CreateCollider(transform.Position);
            _physicsBodyBuilder.AddCollider(collider);
            _physicsBodyBuilder.AddCollisionLayer("ships");
            _physicsBodyBuilder.AddTargetCollisionLayer("asteroids");

            entity.AddComponent(_physicsBodyBuilder.GetResult());
            
            entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });

            var inputEventReceiver = new PlayerInputReceiver(entity);
            _playerInputHandlerKeeper.HandleEvent<Ship>(inputEventReceiver);
        }
    }
}
