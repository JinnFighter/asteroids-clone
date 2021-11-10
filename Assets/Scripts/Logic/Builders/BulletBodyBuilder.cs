using Physics;
using Vector2 = Common.Vector2;

namespace Logic.Builders
{
    public class BulletBodyBuilder : PhysicsBodyBuilder
    {
        public override void AddTransform(Vector2 position, float rotation, Vector2 direction) => PhysicsBody.Transform 
            = new BodyTransform { Position = position, Rotation = rotation, Direction = direction };

        public override void AddRigidBody(float mass, bool useGravity) =>
            PhysicsBody.RigidBody = 
                new PhysicsRigidBody { Mass = mass, Velocity = Vector2.Zero, Force = Vector2.Zero, UseGravity = useGravity };

        public override void AddCollider(Vector2 position) =>
            PhysicsBody.Collider = new BoxPhysicsCollider(position, 7, 7);
    }
}
