namespace Common
{
    public class Ray
    {
        public Vector2 Position { get; set; }

        private Vector2 _direction;
        public Vector2 Direction
        {
            get => _direction;
            private set => _direction = value.Normalized;
        }
        
        public Ray(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
