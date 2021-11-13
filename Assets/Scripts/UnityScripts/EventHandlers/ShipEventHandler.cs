using Logic.Events;
using Logic.Input;
using Physics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ShipEventHandler : IEventHandler<BodyTransform>, IEventHandler<IPlayerInputReceiver>
    {
        private readonly PrefabsContainer _prefabsContainer;
        private readonly ITransformPresenterFactory _transformPresenterFactory;
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;
        private readonly InputEventEmitter _inputEventEmitter;
        private GameObject _gameObject;

        public ShipEventHandler(ITransformPresenterFactory transformPresenterFactory, PrefabsContainer prefabsContainer, 
            PlayerEntitiesDataContainer container, InputEventEmitter inputEventEmitter)
        {
            _transformPresenterFactory = transformPresenterFactory;
            _prefabsContainer = prefabsContainer;
            _playerEntitiesContainer = container;
            _inputEventEmitter = inputEventEmitter;
        }
        
        public void OnCreateEvent(BodyTransform context)
        {
            var position = context.Position;
            _gameObject = Object.Instantiate(_prefabsContainer.ShipPrefab, new Vector2(position.X, position.Y),
                Quaternion.identity);
            
            var presenter = _transformPresenterFactory.CreatePresenter(context, _gameObject.GetComponent<TransformBodyView>());
        }

        public void OnDestroyEvent(BodyTransform context)
        {
        }

        public void OnCreateEvent(IPlayerInputReceiver context)
        {
            var playerInput = _gameObject.GetComponent<PlayerInput>();
            var actionMap = playerInput.currentActionMap;
            _playerEntitiesContainer.AddData(actionMap, context);
            _inputEventEmitter.ListenToInputEvents(actionMap);
        }

        public void OnDestroyEvent(IPlayerInputReceiver context)
        {
        }
    }
}
