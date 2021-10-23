using Logic.EventAttachers;

namespace UnityScripts.InputActions
{
    public class InputActionConverter
    {
        private readonly IEventAttacher _eventAttacher;

        public InputActionConverter(IEventAttacher eventAttacher)
        {
            _eventAttacher = eventAttacher;
        }
        
        public void AttachEvent<T>(in T obj) where T : struct =>
            _eventAttacher.AttachEvent(obj);
    }
}
