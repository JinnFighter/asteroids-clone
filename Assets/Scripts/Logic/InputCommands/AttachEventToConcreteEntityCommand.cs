using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToConcreteEntityCommand<T> : AttachEventToEntityCommand<T> where T : struct
    {
        private readonly T _component;
        private readonly EcsEntity _entity;

        public AttachEventToConcreteEntityCommand(T component, EcsEntity entity) : base(component)
        {
            _entity = entity;
        }

        protected override EcsEntity GetEntity(EcsWorld world) => _entity;
    }
}
