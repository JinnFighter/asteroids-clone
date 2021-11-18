using System.Collections.Generic;
using System.Linq;
using Common;

namespace Physics
{
    public abstract class PhysicsCollider
    {
        public Vector2 Position { get; protected set; }
        public HashSet<int> CollisionLayers { get; } // layers collider is on
        public HashSet<int> TargetCollisionLayers { get; } // layers collider checks collision with

        protected PhysicsCollider()
        {
            TargetCollisionLayers = new HashSet<int>();
            CollisionLayers = new HashSet<int>();
        }

        public abstract void UpdatePosition(float x, float y);

        public bool HasCollision(PhysicsCollider other) =>
            other.CollisionLayers.Intersect(TargetCollisionLayers).Any() &&
            HasCollisionInternal(other);

        protected abstract bool HasCollisionInternal(PhysicsCollider other);

        protected internal abstract bool HasCollisionWithBox(BoxPhysicsCollider other);

        protected internal abstract bool HasCollisionWithRay(RayPhysicsCollider other);

        public abstract int GetQuadTreeIndex(QuadTree quadTree);
    }
}
