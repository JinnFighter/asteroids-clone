using UnityEngine.InputSystem;

namespace UnityScripts.InputActions
{
    public interface IInputActionConverter
    {
        void AcceptConverter(InputActionConverter converter, InputAction inputAction);
    }
}
