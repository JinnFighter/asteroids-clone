namespace Physics
{
    public interface IPhysicsCollider
    {
        void UpdatePosition(Vector2 position);
        bool HasCollision(Vector2 position, IPhysicsCollider other, Vector2 otherPosition);
        bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other, Vector2 otherPosition);
    }
}
