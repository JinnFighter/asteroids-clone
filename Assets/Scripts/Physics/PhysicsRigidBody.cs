using Common;

namespace Physics
{
    public class PhysicsRigidBody
    {
        private Vector2 _velocity;
        public Vector2 Velocity
        {
            get => _velocity;
            set
            {
                _velocity = value;
                VelocityChangedEvent?.Invoke(_velocity);
            }
        }
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;

        public delegate void VelocityChanged(Vector2 velocity);

        public event VelocityChanged VelocityChangedEvent;

        public PhysicsRigidBody()
        {
            Velocity = Vector2.Zero;
            Force = Vector2.Zero;
            Mass = 1f;
            UseGravity = true;
        }
    }
}
