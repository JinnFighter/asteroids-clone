using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitShipRigidBodyHandlersSystem : IEcsInitSystem
    {
        private readonly ShipRigidBodyEventHandlerContainer _shipRigidBodyHandlerContainer;
        private readonly IPhysicsRigidBodyView _rigidBodyView;

        public InitShipRigidBodyHandlersSystem(ShipRigidBodyEventHandlerContainer rigidBodyHandlerContainer,
            IPhysicsRigidBodyView rigidBodyView)
        {
            _shipRigidBodyHandlerContainer = rigidBodyHandlerContainer;
            _rigidBodyView = rigidBodyView;
        }
        
        public void Init(EcsWorld world) 
            => _shipRigidBodyHandlerContainer.AddHandler(new ShipUiRigidBodyEventHandler(new RigidBodyPresenterFactory(),
                _rigidBodyView));
    }
}
