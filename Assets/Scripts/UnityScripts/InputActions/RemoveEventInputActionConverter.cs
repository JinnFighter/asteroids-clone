using UnityEngine.InputSystem;

namespace UnityScripts.InputActions
{
    public class RemoveEventInputActionConverter<T> : IInputActionConverter where T : struct
    {
        public void AcceptConverter(InputActionVisitor visitor, InputAction inputAction) 
            => visitor.RemoveEvent<T>(inputAction.actionMap);
    }
}
