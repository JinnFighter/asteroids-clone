using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultBulletFactory : BulletFactory
    {
        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction)
            => new BodyTransform { Position = position, Rotation = rotation, Direction = direction };

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity)
            => new PhysicsRigidBody { Mass = mass, Force = Vector2.Zero, Velocity = Vector2.Zero, UseGravity = false };

        public override PhysicsCollider CreateCollider(Vector2 position) =>
            new BoxPhysicsCollider(position, 7, 7);
    }
}