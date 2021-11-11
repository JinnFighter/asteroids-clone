using Common;
using Ecs;
using Ecs.Interfaces;
using Logic.Components.GameField;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Factories;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreatePlayerShipSystem : IEcsInitSystem
    {
        private readonly ShipFactory _shipFactory;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public CreatePlayerShipSystem(ShipFactory shipFactory, CollisionLayersContainer collisionLayersContainer)
        {
            _shipFactory = shipFactory;
            _collisionLayersContainer = collisionLayersContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            
            entity.AddComponent(new Ship{ Speed = 1f });
            
            _shipFactory.AddEntity(entity);
            var transform = _shipFactory.CreateTransform(Vector2.Zero, 0f, new Vector2(0, 1));
            var rigidBody = _shipFactory.CreateRigidBody(1f, false);
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
