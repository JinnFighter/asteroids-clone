namespace Physics
{
    public class CustomRigidbody2D
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;

        public CustomRigidbody2D()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Force = Vector2.Zero;
            Mass = 1f;
            UseGravity = false;
        }
    }
}
