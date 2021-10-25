using Ecs;

namespace Logic.InputCommands
{
    public abstract class AttachEventToEntityCommand<T> : IInputCommand where T : struct
    {
        private readonly T _component;
        
        protected AttachEventToEntityCommand(T component)
        {
            _component = component;
        }

        protected abstract EcsEntity GetEntity(EcsWorld world);
    
        public void Execute(EcsWorld world)
        {
            var entity = GetEntity(world);
            entity.AddComponent(_component);
        }
    }
}
