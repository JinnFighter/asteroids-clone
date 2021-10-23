using Logic.Components.Input;
using Logic.EventAttachers;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter<MovementInputAction>
    {
        public void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher) =>
            eventAttacher.AttachEvent(CreateComponent(inputAction));

        public MovementInputAction CreateComponent(InputAction inputAction)
        {
            var direction = inputAction.ReadValue<Vector2>();
            return new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
        }
    }
}
