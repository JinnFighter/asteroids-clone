using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToNewEntityCommand<T> : AttachEventToEntityCommand<T> where T : struct
    {
        private readonly T _component;
        
        public AttachEventToNewEntityCommand(T component) : base(component)
        {
        }

        protected override EcsEntity GetEntity(EcsWorld world) => world.CreateEntity();
    }
}
