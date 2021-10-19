using Ecs;
using Logic.Components.Physics;
using Logic.Factories;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        private readonly IPhysicsObjectFactory _physicsObjectFactory;

        public ShipConveyor(IPhysicsObjectFactory physicsObjectFactory)
        {
            _physicsObjectFactory = physicsObjectFactory;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            var rigidbody2D = _physicsObjectFactory.CreateObject();
            var physicsComponent = new PhysicsBody { Rigidbody2D = rigidbody2D };
            item.AddComponent(physicsComponent);
        }
    }
}
