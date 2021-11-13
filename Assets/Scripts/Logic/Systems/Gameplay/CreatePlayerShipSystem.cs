using Common;
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
    public class CreatePlayerShipSystem : IEcsInitSystem
    {
        private readonly ShipFactory _shipFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;
        private readonly ShipTransformEventHandlerContainer _transformEventHandlerContainer;
        private readonly ShipRigidBodyEventHandlerContainer _rigidBodyEventHandlerRigidBodyEventHandlerContainer;

        public CreatePlayerShipSystem(ShipFactory shipFactory, CollisionLayersContainer collisionLayersContainer, 
            ShipTransformEventHandlerContainer transformEventHandlerContainer, 
            ShipRigidBodyEventHandlerContainer rigidBodyEventHandlerContainer)
        {
            _shipFactory = shipFactory;
            _collisionLayersContainer = collisionLayersContainer;
            _transformEventHandlerContainer = transformEventHandlerContainer;
            _rigidBodyEventHandlerRigidBodyEventHandlerContainer = rigidBodyEventHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            
            entity.AddComponent(new Ship{ Speed = 1f });

            var transform = new BodyTransform { Position = Vector2.Zero, Rotation = 0f, Direction = new Vector2(0, 1) };
            _transformEventHandlerContainer.OnCreateEvent(transform);
            var rigidBody = new PhysicsRigidBody { Mass = 1f, UseGravity = false };
            _rigidBodyEventHandlerRigidBodyEventHandlerContainer.OnCreateEvent(rigidBody);
            var collider = _shipFactory.CreateCollider(transform.Position);
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
        }
    }
}
