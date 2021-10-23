using Logic.Components.Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionConverter converter, InputAction inputAction)
        {
            var direction = inputAction.ReadValue<Vector2>();
            var res = new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
            converter.AttachEvent(res);
        }
    }
}
