using Ecs;
using Ecs.Interfaces;
using Logic.Components.Physics;
using Logic.Conveyors;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreatePlayerShipSystem : IEcsInitSystem
    {
        private readonly ShipConveyor _shipConveyor;

        public CreatePlayerShipSystem(ShipConveyor shipConveyor)
        {
            _shipConveyor = shipConveyor;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            _shipConveyor.UpdateItem(entity);
            var physicsBody = entity.GetComponent<PhysicsBody>();
            physicsBody.Rigidbody2D.Position = Vector2.Zero;
            physicsBody.InvokePositionChangedEvent();
        }
    }
}
