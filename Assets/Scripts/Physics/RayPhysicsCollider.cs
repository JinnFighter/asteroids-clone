using Vector2 = Common.Vector2;

namespace Physics
{
    public class RayPhysicsCollider : PhysicsCollider
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public override void UpdatePosition(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        protected override bool HasCollisionInternal(Vector2 position, PhysicsCollider other, Vector2 otherPosition)
            => other.HasCollisionWithRay(otherPosition, this, position);

        protected internal override bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition)
            => HasCollisionRayAndBox(position, other, otherPosition);

        protected internal override bool HasCollisionWithRay(Vector2 position, RayPhysicsCollider other, Vector2 otherPosition)
        {
            return false;
        }

        public bool HasCollisionRayAndBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition)
        {
            return false;
        }
    }
}
