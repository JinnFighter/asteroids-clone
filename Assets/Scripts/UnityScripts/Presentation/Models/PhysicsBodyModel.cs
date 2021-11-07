using Common.Interfaces;

namespace UnityScripts.Presentation.Models
{
    public class PhysicsBodyModel : IDestroyable
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public delegate void PositionChanged(float x, float y);

        public event PositionChanged PositionChangedEvent;
        
        
        public float Rotation { get; private set; }
        
        public delegate void RotationChanged(float rotation);

        public event RotationChanged RotationChangedEvent;

        public event IDestroyable.OnDestroyEvent DestroyEvent;

        public PhysicsBodyModel(float x, float y)
        {
            X = x;
            Y = y;
            Rotation = 0f;
        }

        public void UpdatePosition(float x, float y)
        {
            X = x;
            Y = y;
            PositionChangedEvent?.Invoke(x, y);
        }
        
        public void UpdateRotation(float rotationAngle)
        {
            Rotation = rotationAngle;
            RotationChangedEvent?.Invoke(Rotation);
        }

        public void Destroy() => DestroyEvent?.Invoke();
    }
}
