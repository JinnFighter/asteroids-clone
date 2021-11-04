namespace Physics
{
    public class PhysicsRigidBody
    {
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;

        public PhysicsRigidBody()
        {
            Velocity = Vector2.Zero;
            Force = Vector2.Zero;
            Mass = 1f;
            UseGravity = true;
        }
    }
}
