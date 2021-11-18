using Logic.Components.Input;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class MovementInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction)
        {
            var res = new MovementInputAction();
            visitor.AttachEvent(inputAction.actionMap, res);
        }
    }
}
