using Logic.Components.Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class LookInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction)
        {
            var inputValue = inputAction.ReadValue<Vector2>();
            var point = Camera.main.ScreenToWorldPoint(inputValue);
            var res = new LookInputAction { LookAtPoint = new Physics.Vector2(point.x, point.y) };
            visitor.AttachEvent(inputAction.actionMap, res);
        }
    }
}
