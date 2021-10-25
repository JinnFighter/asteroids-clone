using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToNewEntityCommand<T> : AttachEventToEntityCommand<T> where T : struct
    {
        private readonly T _component;
        
        public AttachEventToNewEntityCommand(ref T component) : base(ref component)
        {
        }

        protected override EcsEntity GetEntity(EcsWorld world) => world.CreateEntity();
    }
}
