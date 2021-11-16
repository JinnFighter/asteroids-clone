using System.Collections.Generic;
using System.Linq;
using Common;

namespace Physics
{
    public abstract class PhysicsCollider
    {
        public HashSet<int> CollisionLayers { get; } // layers collider is on
        public HashSet<int> TargetCollisionLayers { get; } // layers collider checks collision with

        protected PhysicsCollider()
        {
            TargetCollisionLayers = new HashSet<int>();
            CollisionLayers = new HashSet<int>();
        }

        public abstract void UpdatePosition(float x, float y);

        public bool HasCollision(Vector2 position, PhysicsCollider other, Vector2 otherPosition) =>
            other.CollisionLayers.Intersect(TargetCollisionLayers).Any() &&
            HasCollisionInternal(position, other, otherPosition);

        protected abstract bool HasCollisionInternal(Vector2 position, PhysicsCollider other, Vector2 otherPosition);

        protected internal abstract bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition);

        protected internal abstract bool HasCollisionWithRay(Vector2 position, RayPhysicsCollider other,
            Vector2 otherPosition);
    }
}
