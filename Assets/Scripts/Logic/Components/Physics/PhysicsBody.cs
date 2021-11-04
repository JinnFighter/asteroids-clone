using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        public BodyTransform Transform;

        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;
    }
}
