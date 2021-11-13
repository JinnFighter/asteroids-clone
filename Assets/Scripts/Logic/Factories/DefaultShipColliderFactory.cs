using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultShipColliderFactory : IColliderFactory
    {
        public PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, 10, 10);
    }
}
