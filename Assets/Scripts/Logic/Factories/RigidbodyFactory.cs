using Physics;

namespace Logic.Factories
{
    public class RigidbodyFactory : IRigidbodyFactory
    {
        private readonly PhysicsWorld _physicsWorld;

        public RigidbodyFactory(PhysicsWorld physicsWorld)
        {
            _physicsWorld = physicsWorld;
        }
        
        public CustomRigidbody2D CreateObject()
        {
            var customRigidbody = new CustomRigidbody2D();
            _physicsWorld.AddRigidbody(customRigidbody);
            return customRigidbody;
        }
    }
}
