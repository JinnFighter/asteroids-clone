using Logic.Components.Physics;
using Logic.Events;
using Physics;

namespace Logic.Factories
{
    public class PhysicsBodyBuilder : IPhysicsBodyBuilder
    {
        private PhysicsBody _physicsBody;
        private readonly TransformHandlerKeeper _transformHandlerKeeper;
        private readonly RigidBodyHandlerKeeper _rigidBodyHandlerKeeper;
        private readonly CollisionLayersContainer _collisionLayersContainer;

        public PhysicsBodyBuilder(TransformHandlerKeeper transformHandlerKeeper, RigidBodyHandlerKeeper rigidBodyHandlerKeeper,
            CollisionLayersContainer collisionLayersContainer)
        {
            _transformHandlerKeeper = transformHandlerKeeper;
            _rigidBodyHandlerKeeper = rigidBodyHandlerKeeper;
            _collisionLayersContainer = collisionLayersContainer;
            Reset();
        }
        public void Reset()
        {
            _physicsBody = new PhysicsBody();
        }

        public void AddTransform<T>(TransformBody transformBody) where T : struct
        {
            _physicsBody.Transform = transformBody;
            _transformHandlerKeeper.HandleEvent<T>(transformBody);
        }

        public void AddRigidBody<T>(PhysicsRigidBody rigidBody) where T : struct
        {
            _physicsBody.RigidBody = rigidBody;
            _rigidBodyHandlerKeeper.HandleEvent<T>(rigidBody);
        }

        public void AddCollider(PhysicsCollider collider)
        {
            _physicsBody.Collider = collider;
            _physicsBody.Transform.PositionChangedEvent += collider.UpdatePosition;
        }

        public void AddCollisionLayer(string tag) =>
            _physicsBody.Collider.CollisionLayers.Add(_collisionLayersContainer.GetData(tag));

        public void AddTargetCollisionLayer(string tag) => 
            _physicsBody.Collider.TargetCollisionLayers.Add(_collisionLayersContainer.GetData(tag));
        
        public PhysicsBody GetResult() => _physicsBody;
    }
}
