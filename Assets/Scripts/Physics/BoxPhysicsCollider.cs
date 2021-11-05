namespace Physics
{
    public class BoxPhysicsCollider : PhysicsCollider
    {
        public Vector2 TopLeft { get; private set; }
        public Vector2 DownRight { get; private set; }
        
        public float Width { get; private set; }
        public float Height { get; private set; }

        public BoxPhysicsCollider(Vector2 position, float width, float height)
        {
            Width = width;
            Height = height;
            UpdatePosition(position);
        }

        public sealed override void UpdatePosition(Vector2 position)
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            TopLeft = new Vector2(position.X - halfWidth, position.Y - halfHeight);
            DownRight = new Vector2(position.X + halfWidth, position.Y + halfHeight);
        }

        public override bool HasCollision(Vector2 position, PhysicsCollider other,
            Vector2 otherPosition)
            => other.HasCollisionWithBox(otherPosition, this, position);

        public override bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition)
        {
            var hasIntersection = false;
            return hasIntersection;
        }
    }
}
