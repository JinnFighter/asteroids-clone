using Logic.Components.Input;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class FireInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionConverter converter, InputAction inputAction) =>
            converter.AttachEvent(new FireInputAction());
    }
}
