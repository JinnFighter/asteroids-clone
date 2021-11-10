using Common;
using Logic.Components.Physics;

namespace Logic.Builders
{
    public interface IPhysicsBodyBuilder
    {
        void Reset();
        void AddTransform(Vector2 position, float rotation, Vector2 direction);
        void AddRigidBody(float mass, bool useGravity);
        void AddCollider(Vector2 position);
        ref PhysicsBody GetResult();
    }
}
