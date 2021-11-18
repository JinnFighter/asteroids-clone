using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultBulletColliderFactory : IColliderFactory
    {
        public PhysicsCollider CreateCollider(Vector2 position) =>
            new BoxPhysicsCollider(position, 7, 7);
    }
}
