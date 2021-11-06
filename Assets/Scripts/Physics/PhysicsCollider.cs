using System.Collections.Generic;
using System.Linq;

namespace Physics
{
    public abstract class PhysicsCollider
    {
        private readonly HashSet<int> _collisionLayers; // layers collider is on
        private readonly HashSet<int> _targetCollisionLayers; // layers collider checks collision with

        protected PhysicsCollider(IEnumerable<int> collisionLayers, IEnumerable<int> targetCollisionLayers)
        {
            _targetCollisionLayers = new HashSet<int>(targetCollisionLayers);
            _collisionLayers = new HashSet<int>(collisionLayers);
        }

        public abstract void UpdatePosition(float x, float y);

        public bool HasCollision(Vector2 position, PhysicsCollider other, Vector2 otherPosition) =>
            other._collisionLayers.Intersect(_targetCollisionLayers).Any() &&
            HasCollisionInternal(position, other, otherPosition);

        protected abstract bool HasCollisionInternal(Vector2 position, PhysicsCollider other, Vector2 otherPosition);

        protected internal abstract bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition);
    }
}
