using Common;
using Physics;

namespace Logic.Factories
{
    public interface IPhysicsBodyFactory
    {
        PhysicsCollider CreateCollider(Vector2 position);
    }
}
