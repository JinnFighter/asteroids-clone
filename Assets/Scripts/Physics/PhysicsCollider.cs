namespace Physics
{
    public abstract class PhysicsCollider
    {
        public abstract bool GetCollisionPoints(Vector2 position, PhysicsCollider other, Vector2 otherPosition, out CollisionPoints collisionPoints);
        
        public abstract bool GetPointsFromBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition, out CollisionPoints collisionPoints);
    }
}
