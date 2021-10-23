using Logic.OutboundEvents;
using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;
        
        public delegate void PositionChanged(PositionChangedEvent positionChangedEvent);
        public event PositionChanged PositionChangedEvent;
        
        public void InvokePositionChangedEvent() => PositionChangedEvent?.Invoke(new PositionChangedEvent {X = Position.X, Y = Position.Y });
    }
}
