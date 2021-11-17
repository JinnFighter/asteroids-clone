using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Containers;
using Logic.Events;
using Logic.Input;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreatePlayerShipSystem : IEcsInitSystem
    {
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly ShipTransformEventHandlerContainer _transformEventHandlerContainer;
        private readonly ShipRigidBodyEventHandlerContainer _rigidBodyEventHandlerRigidBodyEventHandlerContainer;
        private readonly PlayerInputHandlerKeeper _playerInputHandlerKeeper;
        private readonly ColliderFactoryContainer _colliderFactoryContainer;

        public CreatePlayerShipSystem(ColliderFactoryContainer colliderFactoryContainer, CollisionLayersContainer collisionLayersContainer, 
            ShipTransformEventHandlerContainer transformEventHandlerContainer, 
            ShipRigidBodyEventHandlerContainer rigidBodyEventHandlerContainer,
            PlayerInputHandlerKeeper playerInputHandlerKeeper)
        {
            _colliderFactoryContainer = colliderFactoryContainer;
            _collisionLayersContainer = collisionLayersContainer;
            _transformEventHandlerContainer = transformEventHandlerContainer;
            _rigidBodyEventHandlerRigidBodyEventHandlerContainer = rigidBodyEventHandlerContainer;
            _playerInputHandlerKeeper = playerInputHandlerKeeper;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            
            entity.AddComponent(new Ship{ Speed = 2f });

            var transform = new BodyTransform { Position = Vector2.Zero, Rotation = 0f, Direction = new Vector2(0, 1) };
            _transformEventHandlerContainer.OnCreateEvent(transform);
            var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
            _rigidBodyEventHandlerRigidBodyEventHandlerContainer.OnCreateEvent(rigidBody);
            var colliderFactory = _colliderFactoryContainer.GetFactory<Ship>();
            var collider = colliderFactory.CreateCollider(transform.Position);
            transform.PositionChangedEvent += collider.UpdatePosition;
            collider.CollisionLayers.Add(_collisionLayersContainer.GetData("ships"));
            collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData("asteroids"));
            
            entity.AddComponent(new PhysicsBody
            {
                Transform = transform,
                RigidBody = rigidBody,
                Collider = collider
            });
            
            entity.AddComponent(new Wrappable{ IsWrappingX = false, IsWrappingY = false });

            var inputEventReceiver = new PlayerInputReceiver(entity);
            _playerInputHandlerKeeper.HandleEvent<Ship>(inputEventReceiver);
        }
    }
}
