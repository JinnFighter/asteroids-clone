using Logic.EventAttachers;
using Logic.InputCommands;

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

        public IInputCommand CreateCommand<T>(object sender, in T obj) where T : struct => null;
    }
}
