using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToNewEntityCommand<T> : IInputCommand where T : struct
    {
        private readonly T _component;

        public AttachEventToNewEntityCommand(ref T component)
        {
            _component = component;
        }
        
        public void Execute(EcsWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComponent(_component);
        }
    }
}
