using Ecs;

namespace Logic.InputCommands
{
    public abstract class AttachEventToEntityCommand<T> : IInputCommand where T : struct
    {
        protected readonly T Component;

        public AttachEventToEntityCommand(ref T component)
        {
            Component = component;
        }

        protected abstract EcsEntity GetEntity(EcsWorld world);
    
        public void Execute(EcsWorld world)
        {
            var entity = GetEntity(world);
            entity.AddComponent(Component);
        }
    }
}
