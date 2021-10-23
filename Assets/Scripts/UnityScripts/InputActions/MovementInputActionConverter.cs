using Logic.Components.Input;
using Logic.EventAttachers;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter
    {
        public void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher)
        {
            var direction = inputAction.ReadValue<Vector2>();
            var component = new MovementInputAction { Direction = new Physics.Vector2(direction.x, direction.y) };
            eventAttacher.AttachEvent(component);
        }
    }
}
