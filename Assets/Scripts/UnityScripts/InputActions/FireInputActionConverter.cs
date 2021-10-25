using Logic.Components.Input;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class FireInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction) =>
            visitor.AttachEvent(inputAction.actionMap, new FireInputAction());
    }
}
