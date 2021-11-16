using Common;

namespace Physics
{
    public sealed class BoxPhysicsCollider : PhysicsCollider
    {
        public Vector2 TopLeft { get; private set; }
        public Vector2 DownRight { get; private set; }
        
        public float Width { get; }
        public float Height { get; }

        public BoxPhysicsCollider(Vector2 position, float width, float height)
        {
            Width = width;
            Height = height;
            UpdatePosition(position.X, position.Y);
        }

        public override void UpdatePosition(float x, float y)
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            TopLeft = new Vector2(x - halfWidth, y - halfHeight);
            DownRight = new Vector2(x + halfWidth, y + halfHeight);
        }

        protected override bool HasCollisionInternal(Vector2 position, PhysicsCollider other,
            Vector2 otherPosition)
            => other.HasCollisionWithBox(otherPosition, this, position);

        protected internal override bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition)
        {
            var d1 = other.TopLeft - DownRight;
            var d2 = TopLeft - other.DownRight;

            return !(d1.X > 0 || d1.Y > 0 || d2.X > 0 || d2.Y > 0);
        }

        protected internal override bool HasCollisionWithRay(Vector2 position, RayPhysicsCollider other,
            Vector2 otherPosition)
            => other.HasCollisionRayAndBox(otherPosition, this, position);
    }
}
