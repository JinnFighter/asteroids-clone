using Ecs;
using Logic.Components.Physics;
using Logic.Conveyors;
using UnityScripts.Presenters;
using UnityScripts.Views;

namespace UnityScripts.Conveyors
{
    public class ShipGameObjectConveyor : EntityConveyor
    {
        private readonly IPhysicsBodyView _physicsBodyView;

        public ShipGameObjectConveyor(IPhysicsBodyView physicsBodyView)
        {
            _physicsBodyView = physicsBodyView;
        }
        
        protected override void UpdateItemInternal(EcsEntity item)
        {
            if (item.HasComponent<PhysicsBody>())
            {
                var physicsBody = item.GetComponent<PhysicsBody>();
                var presenter = new PhysicsBodyPresenter(physicsBody, _physicsBodyView);
            }
        }
    }
}
