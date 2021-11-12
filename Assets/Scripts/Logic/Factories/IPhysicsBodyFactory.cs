using Common;
using Physics;

namespace Logic.Factories
{
    public interface IPhysicsBodyFactory
    {
        BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction);
        PhysicsCollider CreateCollider(Vector2 position);
    }
}
