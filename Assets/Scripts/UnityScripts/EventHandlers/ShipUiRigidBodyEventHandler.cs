using Logic.Events;
using Physics;
using UnityEngine;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipUiRigidBodyEventHandler : IEventHandler<PhysicsRigidBody>
    {
        private readonly IRigidBodyPresenterFactory _rigidBodyPresenterFactory;
        private readonly GameObject _uiGameObject;

        public ShipUiRigidBodyEventHandler(IRigidBodyPresenterFactory rigidBodyPresenterFactory, GameObject uiGameObject)
        {
            _rigidBodyPresenterFactory = rigidBodyPresenterFactory;
            _uiGameObject = uiGameObject;
        }
        
        public void OnCreateEvent(PhysicsRigidBody context)
        {
            var presenter = _rigidBodyPresenterFactory.CreatePresenter(context, _uiGameObject.GetComponent<UiPhysicsRigidBodyView>());
        }

        public void OnDestroyEvent(PhysicsRigidBody context)
        {
        }
    }
}
