using System;
using System.Collections.Generic;
using Logic.EventAttachers;
using Logic.InputCommands;
using Logic.Services;
using UnityEngine.InputSystem;
using UnityScripts.InputActions;
using InputAction = Logic.Components.Input.InputAction;

namespace UnityScripts.EventEmitters
{
    public class InputEventEmitter
    {
        private readonly AsteroidsCloneInputActionAsset _inputActionAsset;
        private readonly Dictionary<Guid, IInputActionConverter> _inputActionConverters;
        private readonly InputActionVisitor _inputActionVisitor;

        private readonly InputCommandQueue _inputCommandQueue;

        public InputEventEmitter(IEventAttacher eventAttacher, InputCommandQueue inputCommandQueue)
        {
            _inputCommandQueue = inputCommandQueue;

            _inputActionVisitor = new InputActionVisitor(eventAttacher);

            _inputActionAsset = new AsteroidsCloneInputActionAsset();

            _inputActionConverters = new Dictionary<Guid, IInputActionConverter>();
            var playerActions = _inputActionAsset.Player;
            _inputActionConverters.Add(playerActions.Look.id, new LookInputActionConverter());
            _inputActionConverters.Add(playerActions.Move.id, new MovementInputActionConverter());
            _inputActionConverters.Add(playerActions.Fire.id, new FireInputActionConverter());
        }

        private void CreateInputEvent(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var action = context.action;
            if(_inputActionConverters.TryGetValue(action.id, out var inputActionConverter))
                inputActionConverter.AcceptConverter(_inputActionVisitor, action);
            else
            {
                var inputAction = new InputAction { ActionName = action.name, ActionMapName = action.actionMap.name };
                _inputCommandQueue.Enqueue(new AttachEventToNewEntityCommand<InputAction>(ref inputAction));
            }
        }

        public void ListenToInputEvents(InputActionMap actionMap)
        {
            foreach (var action in actionMap)
            {
                action.performed += CreateInputEvent;
                action.Enable();
            }
        }
    }
}
