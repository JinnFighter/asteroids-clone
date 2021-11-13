using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public abstract class AsteroidColliderFactory : IColliderFactory
    {
        public abstract PhysicsCollider CreateCollider(Vector2 position);
    }
}
