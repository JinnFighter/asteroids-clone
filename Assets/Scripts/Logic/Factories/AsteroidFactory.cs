using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public abstract class AsteroidFactory : IPhysicsBodyFactory
    {
        protected int Stage;

        public abstract void SetStage(int stage);

        public abstract BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction);

        public abstract PhysicsCollider CreateCollider(Vector2 position);
    }
}
