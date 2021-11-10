using Logic.Components.Physics;
using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Builders
{
    public abstract class PhysicsBodyBuilder : IPhysicsBodyBuilder
    {
        protected PhysicsBody PhysicsBody;

        public void Reset() => PhysicsBody = new PhysicsBody();

        public abstract void AddTransform(Vector2 position, float rotation, Vector2 direction);

        public abstract void AddRigidBody(float mass, bool useGravity);

        public abstract void AddCollider(Vector2 position);

        public ref PhysicsBody GetResult() => ref PhysicsBody;
    }
}
