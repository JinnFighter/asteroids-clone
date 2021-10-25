using Logic.EventAttachers;

namespace UnityScripts.InputActions
{
    public class InputActionVisitor
    {
        private readonly IEventAttacher _eventAttacher;

        public InputActionVisitor(IEventAttacher eventAttacher)
        {
            _eventAttacher = eventAttacher;
        }
        
        public void AttachEvent<T>(object sender, in T obj) where T : struct =>
            _eventAttacher.AttachEvent(sender, obj);
    }
}
