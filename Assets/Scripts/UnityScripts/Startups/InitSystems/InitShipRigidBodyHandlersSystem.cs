using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitShipRigidBodyHandlersSystem : IEcsInitSystem
    {
        private readonly RigidBodyHandlerKeeper _rigidBodyHandlerKeeper;
        private readonly IPhysicsRigidBodyView _rigidBodyView;

        public InitShipRigidBodyHandlersSystem(RigidBodyHandlerKeeper rigidBodyHandlerKeeper,
            IPhysicsRigidBodyView rigidBodyView)
        {
            _rigidBodyHandlerKeeper = rigidBodyHandlerKeeper;
            _rigidBodyView = rigidBodyView;
        }
        
        public void Init(EcsWorld world) 
            => _rigidBodyHandlerKeeper.AddHandler<Ship>(new ShipUiRigidBodyEventHandler(new RigidBodyPresenterFactory(),
                _rigidBodyView));
    }
}
