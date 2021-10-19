using Ecs;
using Logic.Components.Physics;
using Logic.Factories;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        private readonly IRigidbodyFactory _rigidbodyFactory;

        public ShipConveyor(IRigidbodyFactory rigidbodyFactory)
        {
            _rigidbodyFactory = rigidbodyFactory;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            var rigidbody2D = _rigidbodyFactory.CreateObject();
            var physicsComponent = new PhysicsBody { Rigidbody2D = rigidbody2D };
            item.AddComponent(physicsComponent);
        }
    }
}
