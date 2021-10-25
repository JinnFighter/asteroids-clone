using Logic.Components.Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction)
        {
            var direction = inputAction.ReadValue<Vector2>();
            var res = new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
            visitor.AttachEvent(inputAction.actionMap, res);
        }
    }
}
