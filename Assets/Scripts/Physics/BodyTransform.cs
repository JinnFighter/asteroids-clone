using System;
using Common;

namespace Physics
{
    public class BodyTransform
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
    
        public delegate void PositionChanged(float x, float y);
        public event PositionChanged PositionChangedEvent;


        private float _rotation;
        public float Rotation
        {
            get => _rotation;
            set
            {
                var oldRotation = _rotation;
                _rotation = value;
                if (Math.Abs(_rotation - oldRotation) > 0.000005f)
                    RotationChangedEvent?.Invoke(_rotation);
            }
        }

        public delegate void RotationChanged(float rotation);

        public event RotationChanged RotationChangedEvent;
        
        
        private Vector2 _direction;
        
        public Vector2 Direction
        {
            get => _direction;
            set => _direction = value.Normalized;
        }
    }
}
