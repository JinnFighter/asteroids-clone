using Logic.Components.Input;
using Logic.EventAttachers;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace UnityScripts.InputActions
{
    public class FireInputActionConverter : IInputActionConverter
    {
        public void ConvertInputAction(InputAction inputAction, IEventAttacher eventAttacher)
        {
            var component = new FireInputAction();
            eventAttacher.AttachEvent(component);
        }
    }
}
