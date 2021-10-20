using Logic.EventAttachers;
using UnityEngine.InputSystem;
using InputAction = Logic.Components.Input.InputAction;

namespace UnityScripts.EventEmitters
{
    public class InputEventEmitter
    {
        private readonly IEventAttacher _eventAttacher;
        private readonly PlayerInput _playerInput;

        public InputEventEmitter(IEventAttacher eventAttacher, PlayerInput playerInput)
        {
            _eventAttacher = eventAttacher;
            _playerInput = playerInput;

            foreach (var action in _playerInput.actions)
            {
                action.performed += CreateInputEvent;
                action.Enable();
            }
        }

        private void CreateInputEvent(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var action = context.action;
            var eventObject = new InputAction { ActionName = action.name, ActionMapName = action.actionMap.name };
            _eventAttacher.AttachEvent(eventObject);
        }
    }
}
