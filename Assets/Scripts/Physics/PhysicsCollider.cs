namespace Physics
{
    public abstract class PhysicsCollider
    {
        public abstract void UpdatePosition(Vector2 position);
        public abstract bool HasCollision(Vector2 position, PhysicsCollider other, Vector2 otherPosition);
        
        public abstract bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition);
    }
}
