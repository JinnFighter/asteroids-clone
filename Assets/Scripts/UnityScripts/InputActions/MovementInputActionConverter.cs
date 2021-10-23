using Logic.Components.Input;
using Logic.EventAttachers;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter<MovementInputAction>
    {
        public void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher)
        {
            var direction = inputAction.ReadValue<Vector2>();
            var component = new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
            eventAttacher.AttachEvent(component);
        }

        public MovementInputAction CreateComponent(InputAction inputAction)
        {
            var direction = inputAction.ReadValue<Vector2>();
            return new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
        }
    }
}
