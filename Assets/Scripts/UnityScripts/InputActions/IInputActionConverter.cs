using UnityEngine.InputSystem;

namespace UnityScripts.InputActions
{
    public interface IInputActionConverter
    {
        void AcceptConverter(InputActionVisitor visitor, InputAction inputAction);
    }
}
