using Common;
using Vector2 = Common.Vector2;

namespace Physics
{
    public class RayPhysicsCollider : PhysicsCollider
    {
        public readonly Ray Ray;

        public RayPhysicsCollider(Vector2 position, Vector2 direction)
        {
            Ray = new Ray(position, direction);
            Position = position;
        }

        public override void UpdatePosition(float x, float y)
        {
            Position = new Vector2(x, y);
            Ray.Position = Position;
        }

        protected override bool HasCollisionInternal(PhysicsCollider other)
            => other.HasCollisionWithRay(this);

        protected internal override bool HasCollisionWithBox(BoxPhysicsCollider other) =>
            Geometry.HasIntersectionRayAndRectangle(Ray.Direction, other.Rectangle.Min, other.Rectangle.Max);

        protected internal override bool HasCollisionWithRay(RayPhysicsCollider other) => false;

        public override int GetQuadTreeIndex(QuadTree quadTree) => quadTree.GetRayIndex(this);
    }
}
