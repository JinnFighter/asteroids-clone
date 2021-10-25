using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToConcreteEntityCommand<T> : AttachEventToEntityCommand<T> where T : struct
    {
        private readonly T _component;
        private readonly EcsEntity _entity;

        public AttachEventToConcreteEntityCommand(ref T component) : base(ref component)
        {
        }

        protected override EcsEntity GetEntity(EcsWorld world) => _entity;
    }
}
