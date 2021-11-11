using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Factories
{
    public class DefaultShipFactory : ShipFactory
    {
        public override BodyTransform CreateTransform(Vector2 position, float rotation, Vector2 direction)
            => new BodyTransform { Position = position, Rotation = rotation, Direction = direction };

        public override PhysicsRigidBody CreateRigidBody(float mass, bool useGravity) =>
            new PhysicsRigidBody { Mass = mass, UseGravity = false };

        public override PhysicsCollider CreateCollider(Vector2 position) => new BoxPhysicsCollider(position, 10, 10);
    }
}
