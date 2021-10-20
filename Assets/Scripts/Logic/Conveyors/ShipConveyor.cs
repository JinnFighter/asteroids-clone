using Ecs;
using Logic.Components.Physics;
using Logic.Factories;
using Physics;

namespace Logic.Conveyors
{
    public class ShipConveyor : EntityConveyor
    {
        private readonly IFactory<CustomRigidbody2D> _rigidbodyFactory;

        public ShipConveyor(IFactory<CustomRigidbody2D> rigidbodyFactory)
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
