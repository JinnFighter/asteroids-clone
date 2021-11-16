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
            float GetBestIntersection(float directionCoord, float minCoord, float maxCoord)
            {
                if (directionCoord > 0)
                    return (minCoord - directionCoord) / directionCoord;
                if (directionCoord < 0)
                    return (maxCoord - directionCoord) / directionCoord;

                return -1;
            }

            var bestX = GetBestIntersection(Direction.X, other.TopLeft.X, other.DownRight.X);
            var bestY = GetBestIntersection(Direction.Y, other.TopLeft.Y, other.DownRight.Y);

            var bestIntersection = Math.Max(bestX, bestY);
            if (bestIntersection < 0f)
                return false;

            var intersection = Position + Direction * bestIntersection;
            return !(intersection.X < other.TopLeft.X) && !(intersection.X > other.DownRight.X) 
                                                       && !(intersection.Y < other.TopLeft.Y)
                   && !(intersection.Y > other.DownRight.Y);
        }
    }
}
