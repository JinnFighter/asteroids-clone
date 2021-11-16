using System;
using Vector2 = Common.Vector2;

namespace Physics
{
    public class RayPhysicsCollider : PhysicsCollider
    {
        public Vector2 Position { get; private set; }

        private Vector2 _direction;
        public Vector2 Direction
        {
            get => _direction;
            private set => _direction = value.Normalized;
        }

        public RayPhysicsCollider(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }

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
            var point1 = other.TopLeft - Direction;
            var point2 = other.DownRight - Direction;

            var t1 = new Vector2(point1.X * Direction.X, point1.Y * Direction.Y);
            var t2 = new Vector2(point2.X * Direction.X, point2.Y * Direction.Y);

            var tmin = Math.Min(t1.X, t2.X);
            var tmax = Math.Max(t1.X, t2.X);

            tmin = Math.Max(tmin, Math.Min(t1.Y, t2.Y));
            tmax = Math.Min(tmax, Math.Max(t1.Y, t2.Y));

            return tmax >= tmin;
        }
    }
}
