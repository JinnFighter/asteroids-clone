using Logic.Components.Input;
using UnityEngine.InputSystem;

namespace UnityScripts.InputActions
{
    public class LaserFireInputActionConverter : IInputActionConverter
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction)
            => visitor.AttachEvent(inputAction.actionMap, new LaserFireInputAction());
    }
}
