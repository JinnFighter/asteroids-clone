using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultAsteroidFactory : AsteroidFactory
    {
        public override void SetStage(int stage) => Stage = stage;

        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction) 
            => new BodyTransform { Position = position, Direction = direction };

        public override PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, 10, 10);
    }
}
