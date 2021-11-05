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
        
        public override bool HasCollision(Vector2 position, PhysicsCollider other,
            Vector2 otherPosition)
            => other.HasCollisionWithBox(otherPosition, this, position);

        public override bool HasCollisionWithBox(Vector2 position, BoxPhysicsCollider other,
            Vector2 otherPosition)
        {
            var hasIntersection = false;
            return hasIntersection;
        }
    }
}
