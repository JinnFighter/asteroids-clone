using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultLaserColliderFactory : IColliderFactory
    {
        public PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, 72, 32);
    }
}
