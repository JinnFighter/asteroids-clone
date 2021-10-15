using Logic.OutboundEvents;
using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        public CustomRigidbody2D Rigidbody2D;
        public delegate void PositionChanged(PositionChangedEvent positionChangedEvent);
        public event PositionChanged PositionChangedEvent;
    }
}
