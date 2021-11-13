using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultAsteroidFactory : AsteroidFactory
    {
        public override PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, 10, 10);
    }
}
