using Ecs;

namespace Logic.InputCommands
{
    public class AttachEventToConcreteEntityCommand<T> : IInputCommand where T : struct
    {
        private readonly T _component;
        private readonly EcsEntity _entity;

        public AttachEventToConcreteEntityCommand(ref T component, EcsEntity entity)
        {
            _component = component;
            _entity = entity;
        }

        public void Execute(EcsWorld world) => _entity.AddComponent(_component);
    }
}
