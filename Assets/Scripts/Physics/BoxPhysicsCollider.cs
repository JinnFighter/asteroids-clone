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
        
        public override CollisionPoints GetCollisionPoints(Vector2 position, PhysicsCollider other,
            Vector2 otherPosition)
            => other.GetPointsFromBox(otherPosition, this, position);

        public override CollisionPoints GetPointsFromBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition) => new CollisionPoints();
    }
}
