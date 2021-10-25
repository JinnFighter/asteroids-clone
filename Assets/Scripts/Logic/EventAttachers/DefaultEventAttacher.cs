using Ecs;

namespace Logic.EventAttachers
{
    public class DefaultEventAttacher : IEventAttacher
    {
        private readonly EcsWorld _ecsWorld;

        public DefaultEventAttacher(EcsWorld world)
        {
            _ecsWorld = world;
        }

        public void AttachEvent<T>(object sender, T eventObject) where T : struct
        {
            var entity = _ecsWorld.CreateEntity();
            entity.AddComponent(eventObject);
        }
    }
}
