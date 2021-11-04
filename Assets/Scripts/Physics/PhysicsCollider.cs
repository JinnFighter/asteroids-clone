namespace Physics
{
    public abstract class PhysicsCollider
    {
        public abstract CollisionPoints GetCollisionPoints(Vector2 position, PhysicsCollider other, Vector2 otherPosition);
    }
}
