using Ecs;
using Logic.Components.Physics;
using Physics;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        private readonly PhysicsWorld _physicsWorld;

        public ShipConveyor(PhysicsWorld physicsWorld)
        {
            _physicsWorld = physicsWorld;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            var rigidbody2D = new CustomRigidbody2D();
            _physicsWorld.AddRigidbody(rigidbody2D);
            var physicsComponent = new PhysicsBody { Rigidbody2D = rigidbody2D };
            item.AddComponent(physicsComponent);
        }
    }
}
