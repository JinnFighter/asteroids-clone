namespace UnityScripts.Presentation.Models
{
    public class PhysicsBodyModel
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public delegate void PositionChanged(float x, float y);

        public event PositionChanged PositionChangedEvent;

        public PhysicsBodyModel(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void UpdatePosition(float x, float y)
        {
            X = x;
            Y = y;
            PositionChangedEvent?.Invoke(x, y);
        }
    }
}
