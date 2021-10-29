using System;
using System.Collections.Generic;
using Logic.Services;
using UnityEngine.InputSystem;
using UnityScripts.Containers;
using UnityScripts.InputActions;

namespace UnityScripts.EventEmitters
{
    public class InputEventEmitter
    {
        private readonly AsteroidsCloneInputActionAsset _inputActionAsset;
        private readonly Dictionary<Guid, IInputActionConverter> _inputActionConverters;
        private readonly InputActionVisitor _inputActionVisitor;

        public InputEventEmitter(PlayerEntitiesDataContainer container, InputCommandQueue inputCommandQueue)
        {
            _inputActionVisitor = new InputActionVisitor(container, inputCommandQueue);

            _inputActionAsset = new AsteroidsCloneInputActionAsset();

            _inputActionConverters = new Dictionary<Guid, IInputActionConverter>();
            var playerActions = _inputActionAsset.Player;
            _inputActionConverters.Add(playerActions.Look.id, new LookInputActionConverter());
            _inputActionConverters.Add(playerActions.Move.id, new MovementInputActionConverter());
            _inputActionConverters.Add(playerActions.Fire.id, new FireInputActionConverter());
        }

        private void CreateInputEvent(InputAction.CallbackContext context)
        {
            var action = context.action;
            if(_inputActionConverters.TryGetValue(action.id, out var inputActionConverter))
                inputActionConverter.AcceptConverter(_inputActionVisitor, action);
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
