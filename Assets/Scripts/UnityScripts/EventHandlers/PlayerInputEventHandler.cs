using Logic.Events;
using Logic.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.EventEmitters;

namespace UnityScripts.EventHandlers
{
    public class PlayerInputEventHandler : IEventHandler<IPlayerInputReceiver>, IEventHandler<GameObject>
    {
        private readonly PlayerEntitiesDataContainer _playerEntitiesContainer;
        private readonly InputEventEmitter _inputEventEmitter;

        public PlayerInputEventHandler(PlayerEntitiesDataContainer playerEntitiesDataContainer,
            InputEventEmitter inputEventEmitter)
        {
            _playerEntitiesContainer = playerEntitiesDataContainer;
            _inputEventEmitter = inputEventEmitter;
        }
        private PlayerInput _playerInput;
        
        public void OnCreateEvent(IPlayerInputReceiver context)
        {
            var actionMap = _playerInput.currentActionMap;
            _playerEntitiesContainer.AddData(actionMap, context);
            _inputEventEmitter.ListenToInputEvents(actionMap);
        }

        public void OnCreateEvent(GameObject context) => _playerInput = context.GetComponent<PlayerInput>();
    }
}
