using Common;

namespace Logic.Components.Gameplay
{
    public struct CreateAsteroidEvent
    {
        public int Stage;
        public Vector2 Position;
        public float Mass;
        public Vector2 Direction;
    }
}
