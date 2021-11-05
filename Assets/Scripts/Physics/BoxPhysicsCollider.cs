namespace Physics
{
    public class BoxPhysicsCollider : IPhysicsCollider
    {
        public Vector2 TopLeft { get; private set; }
        public Vector2 DownRight { get; private set; }
        
        public float Width { get; }
        public float Height { get; }

        public BoxPhysicsCollider(Vector2 position, float width, float height)
        {
            Width = width;
            Height = height;
            UpdatePosition(position);
        }

        public void UpdatePosition(Vector2 position)
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            TopLeft = new Vector2(position.X - halfWidth, position.Y - halfHeight);
            DownRight = new Vector2(position.X + halfWidth, position.Y + halfHeight);
        }

        public bool HasCollision(Vector2 position, IPhysicsCollider other,
            Vector2 otherPosition)
            => other.HasCollisionWithBox(otherPosition, this, position);

        public bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition)
        {
            var d1 = other.TopLeft - DownRight;
            var d2 = TopLeft - other.DownRight;

            return !(d1.X > 0 || d1.Y > 0 || d2.X > 0 || d2.Y > 0);
        }
    }
}
