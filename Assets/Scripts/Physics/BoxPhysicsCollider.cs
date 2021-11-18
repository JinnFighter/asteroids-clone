using Common;

namespace Physics
{
    public sealed class BoxPhysicsCollider : PhysicsCollider
    {
        public readonly Rectangle Rectangle;

        public BoxPhysicsCollider(Vector2 position, float width, float height)
        {
            Rectangle = new Rectangle(position, width, height);
            UpdatePosition(position.X, position.Y);
        }

        public override void UpdatePosition(float x, float y)
        {
            Rectangle.Position = new Vector2(x, y);
            Position = new Vector2(x, y);
        }

        protected override bool HasCollisionInternal(PhysicsCollider other)
            => other.HasCollisionWithBox(this);

        protected internal override bool HasCollisionWithBox(BoxPhysicsCollider other)
        {
            var d1 = other.Rectangle.Min - Rectangle.Max;
            var d2 = Rectangle.Min - other.Rectangle.Max;

            return !(d1.X > 0 || d1.Y > 0 || d2.X > 0 || d2.Y > 0);
        }

        protected internal override bool HasCollisionWithRay(RayPhysicsCollider other)
            => other.HasCollisionRayAndBox(this);

        public override int GetQuadTreeIndex(QuadTree quadTree) => quadTree.GetBoxIndex(this);
    }
}
