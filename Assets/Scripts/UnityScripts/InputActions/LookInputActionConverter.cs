using Logic.Components.Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class LookInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionConverter converter, InputAction inputAction)
        {
            var point = inputAction.ReadValue<Vector2>();
            var res = new LookInputAction { LookAtPoint = new Physics.Vector2(point.x, point.y) };
            converter.AttachEvent(res);
        }
    }
}
