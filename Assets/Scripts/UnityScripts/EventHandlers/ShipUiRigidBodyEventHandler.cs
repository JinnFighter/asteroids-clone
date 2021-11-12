using System.Collections.Generic;
using Logic.Events;
using Physics;
using UnityEngine;
using UnityScripts.Factories;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipUiRigidBodyEventHandler : IEventHandler<PhysicsRigidBody>
    {
        private readonly IRigidBodyPresenterFactory _rigidBodyPresenterFactory;
        private readonly Dictionary<PhysicsRigidBody, List<PhysicsRigidBodyPresenter>> _presenters;
        private readonly GameObject _uiGameObject;

        public ShipUiRigidBodyEventHandler(IRigidBodyPresenterFactory rigidBodyPresenterFactory, GameObject uiGameObject)
        {
            _rigidBodyPresenterFactory = rigidBodyPresenterFactory;
            _uiGameObject = uiGameObject;
            _presenters = new Dictionary<PhysicsRigidBody, List<PhysicsRigidBodyPresenter>>();
        }
        
        public void OnCreateEvent(PhysicsRigidBody context)
        {
            var presenter = _rigidBodyPresenterFactory.CreatePresenter(context, _uiGameObject.GetComponent<UiPhysicsRigidBodyView>());
            List<PhysicsRigidBodyPresenter> contextedPresentersList;
            if (_presenters.ContainsKey(context))
                contextedPresentersList = _presenters[context];
            else
            {
                contextedPresentersList = new List<PhysicsRigidBodyPresenter>();
                _presenters[context] = contextedPresentersList;
            }
            
            contextedPresentersList.Add(presenter);
        }

        public void OnDestroyEvent(PhysicsRigidBody context)
        {
            foreach (var presenter in _presenters[context])
                presenter.Destroy();
        }
    }
}
