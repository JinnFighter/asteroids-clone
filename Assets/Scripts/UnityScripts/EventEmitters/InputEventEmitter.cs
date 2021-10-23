using System.Collections.Generic;
using Logic.EventAttachers;
using UnityEngine.InputSystem;
using UnityScripts.InputActions;
using InputAction = Logic.Components.Input.InputAction;

namespace UnityScripts.EventEmitters
{
    public class InputEventEmitter
    {
        private readonly IEventAttacher _eventAttacher;
        private readonly AsteroidsCloneInputActionAsset _inputActionAsset;
        private readonly Dictionary<string, IInputActionConverter> _inputActionConverters;
        private readonly InputActionConverter _inputActionConverter;

        public InputEventEmitter(IEventAttacher eventAttacher, PlayerInput playerInput)
        {
            _eventAttacher = eventAttacher;

            _inputActionConverter = new InputActionConverter(eventAttacher);

            _inputActionAsset = new AsteroidsCloneInputActionAsset();

            _inputActionConverters = new Dictionary<string, IInputActionConverter>();
            var playerActions = _inputActionAsset.Player;
            _inputActionConverters.Add(playerActions.Look.name, new LookInputActionConverter());
            _inputActionConverters.Add(playerActions.Move.name, new MovementInputActionConverter());
            _inputActionConverters.Add(playerActions.Fire.name, new FireInputActionConverter());

            foreach (var action in playerInput.actions)
            {
                action.performed += CreateInputEvent;
                action.Enable();
            }
        }

        private void CreateInputEvent(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var action = context.action;
            if(_inputActionConverters.TryGetValue(action.name, out var inputActionConverter))
                inputActionConverter.AcceptConverter(_inputActionConverter, action);
            else
                _eventAttacher.AttachEvent
                    (new InputAction { ActionName = action.name, ActionMapName = action.actionMap.name });
        }
    }
}
