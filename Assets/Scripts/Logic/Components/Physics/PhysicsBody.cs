using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        public CustomRigidbody2D Rigidbody2D;
        public delegate void PositionChanged(Vector2 position);
        public event PositionChanged PositionChangedEvent;
    }
}
