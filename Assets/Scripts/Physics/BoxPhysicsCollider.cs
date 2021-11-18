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

        protected internal override bool HasCollisionWithBox(BoxPhysicsCollider other) =>
            Geometry.HasIntersectionRectangleAndRectangle(Rectangle, other.Rectangle);

        protected internal override bool HasCollisionWithRay(RayPhysicsCollider other)
            => Geometry.HasIntersectionRayAndRectangle(other.Ray.Direction, Rectangle.Min, Rectangle.Max);

        public override int GetQuadTreeIndex(QuadTree quadTree) => quadTree.GetBoxIndex(this);
    }
}
