using Physics;

namespace Logic.Components.Physics
{
    public struct PhysicsBody
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            set
            {
                var oldPosition = _position;
                _position = value;
                if(!_position.Equals(oldPosition))
                    PositionChangedEvent?.Invoke(_position.X, _position.Y);
            }
        }
        
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public bool UseGravity;
        
        public delegate void PositionChanged(float x, float y);
        public event PositionChanged PositionChangedEvent;
    }
}
