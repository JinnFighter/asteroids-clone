using Physics;

namespace Logic.Factories
{
    public class PhysicsObjectFactory : IPhysicsObjectFactory
    {
        private readonly PhysicsWorld _physicsWorld;

        public PhysicsObjectFactory(PhysicsWorld physicsWorld)
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
