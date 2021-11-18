using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        public TransformBody Transform;
        public PhysicsRigidBody RigidBody;
        public PhysicsCollider Collider;
    }
}
