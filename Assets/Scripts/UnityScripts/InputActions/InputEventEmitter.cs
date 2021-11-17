using System;
using System.Collections.Generic;
using Logic.Components.Input;
using Logic.Services;
using UnityEngine.InputSystem;
using UnityScripts.Containers;

namespace UnityScripts.InputActions
{
    public class InputEventEmitter
    {
        private readonly AsteroidsCloneInputActionAsset _inputActionAsset;
        private readonly Dictionary<Guid, IInputActionConverter> _startedInputActionConverters;
        private readonly Dictionary<Guid, IInputActionConverter> _performedInputActionConverters;
        private readonly Dictionary<Guid, IInputActionConverter> _cancelledInputActionConverters;
        private readonly InputActionVisitor _inputActionVisitor;

        public InputEventEmitter(PlayerEntitiesDataContainer container, InputCommandQueue inputCommandQueue)
        {
            _inputActionVisitor = new InputActionVisitor(container, inputCommandQueue);

            _inputActionAsset = new AsteroidsCloneInputActionAsset();

            _startedInputActionConverters = new Dictionary<Guid, IInputActionConverter>();
            _performedInputActionConverters = new Dictionary<Guid, IInputActionConverter>();
            _cancelledInputActionConverters = new Dictionary<Guid, IInputActionConverter>();
            var playerActions = _inputActionAsset.Player;
            
            _startedInputActionConverters.Add(playerActions.Move.id, new MovementInputActionConverter());
            
            _performedInputActionConverters.Add(playerActions.Look.id, new LookInputActionConverter());
            _performedInputActionConverters.Add(playerActions.Move.id, new RemoveEventInputActionConverter<MovementInputAction>());
            _performedInputActionConverters.Add(playerActions.Fire.id, new FireInputActionConverter());
            _performedInputActionConverters.Add(playerActions.LaserFire.id, new LaserFireInputActionConverter());
        }
        
        private void CreateEvent(InputAction action,
            IReadOnlyDictionary<Guid, IInputActionConverter> converters)
        {
            if(converters.TryGetValue(action.id, out var inputActionConverter))
                inputActionConverter.AcceptConverter(_inputActionVisitor, action);
        }

        private void CreateStartedInputEvent(InputAction.CallbackContext context) =>
            CreateEvent(context.action, _startedInputActionConverters);

        private void CreatePerformedInputEvent(InputAction.CallbackContext context) =>
            CreateEvent(context.action, _performedInputActionConverters);
        
        private void CreateCancelledInputEvent(InputAction.CallbackContext context) =>
            CreateEvent(context.action, _cancelledInputActionConverters);

        public void ListenToInputEvents(InputActionMap actionMap)
        {
            foreach (var action in actionMap)
            {
                action.started += CreateStartedInputEvent;
                action.performed += CreatePerformedInputEvent;
                action.canceled += CreateCancelledInputEvent;
                
                action.Enable();
            }
        }
    }
}
