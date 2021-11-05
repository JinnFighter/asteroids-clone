namespace Physics
{
    public class BoxPhysicsCollider : PhysicsCollider
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public BoxPhysicsCollider(float width, float height)
        {
            Width = width;
            Height = height;
        }
        
        public override bool GetCollisionPoints(Vector2 position, PhysicsCollider other,
            Vector2 otherPosition, out CollisionPoints collisionPoints)
            => other.GetPointsFromBox(otherPosition, this, position, out collisionPoints);

        public override bool GetPointsFromBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition, out CollisionPoints collisionPoints)
        {
            collisionPoints = new CollisionPoints();
            return false;
        }
    }
}
