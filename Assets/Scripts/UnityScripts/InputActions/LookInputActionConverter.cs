using Logic.Components.Input;
using Logic.EventAttachers;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class LookInputActionConverter : IInputActionConverter<LookInputAction>
    {
        public void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher) =>
            eventAttacher.AttachEvent(CreateComponent(inputAction));

        public LookInputAction CreateComponent(InputAction inputAction)
        {
            var point = inputAction.ReadValue<Vector2>();
            return new LookInputAction { LookAtPoint = new Physics.Vector2(point.x, point.y) };
        }
    }
}
