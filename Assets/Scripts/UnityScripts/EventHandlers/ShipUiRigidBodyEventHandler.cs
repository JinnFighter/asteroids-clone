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
        private readonly IPhysicsRigidBodyView _rigidBodyView;

        public ShipUiRigidBodyEventHandler(IRigidBodyPresenterFactory rigidBodyPresenterFactory, IPhysicsRigidBodyView rigidBodyView)
        {
            _rigidBodyPresenterFactory = rigidBodyPresenterFactory;
            _rigidBodyView = rigidBodyView;
        }
        
        public void Handle(PhysicsRigidBody context) => _rigidBodyPresenterFactory.CreatePresenter(context, _rigidBodyView);
    }
}
