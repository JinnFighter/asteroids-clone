using Logic.EventAttachers;
using UnityEngine.InputSystem;
using InputAction = Logic.Components.Input.InputAction;

namespace UnityScripts.EventEmitters
{
    public class InputEventEmitter
    {
        private readonly IEventAttacher _eventAttacher;

        public InputEventEmitter(IEventAttacher eventAttacher, PlayerInput playerInput)
        {
            _eventAttacher = eventAttacher;

            foreach (var action in playerInput.actions)
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
