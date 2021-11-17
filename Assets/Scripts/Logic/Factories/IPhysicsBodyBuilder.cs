using Logic.Components.Physics;
using Physics;

namespace Logic.Factories
{
    public interface IPhysicsBodyBuilder
    {
        void Reset();
        void AddTransform<T>(TransformBody transformBody) where T : struct;
        void AddRigidBody<T>(PhysicsRigidBody rigidBody) where T : struct;
        void AddCollider(PhysicsCollider collider);
        void AddCollisionLayer(string tag);
        void AddTargetCollisionLayer(string tag);
        PhysicsBody GetResult();
    }
}
