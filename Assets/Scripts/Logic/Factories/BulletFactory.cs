using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public abstract class BulletFactory : IPhysicsBodyFactory
    {
        public abstract BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction);

        public abstract PhysicsCollider CreateCollider(Vector2 position);
    }
}
