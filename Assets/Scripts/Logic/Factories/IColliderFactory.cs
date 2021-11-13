using Common;
using Physics;

namespace Logic.Factories
{
    public interface IColliderFactory
    {
        PhysicsCollider CreateCollider(Vector2 position);
    }
}
