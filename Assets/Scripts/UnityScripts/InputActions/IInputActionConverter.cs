using Logic.EventAttachers;
using UnityEngine.InputSystem;

namespace UnityScripts.InputActions
{
    public interface IInputActionConverter<out T> where T : struct
    {
        void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher);
        T CreateComponent(InputAction inputAction);
    }
}
