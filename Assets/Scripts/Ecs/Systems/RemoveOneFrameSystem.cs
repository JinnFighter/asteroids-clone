using Ecs.Interfaces;

namespace Ecs.Systems
{
    internal class RemoveOneFrameSystem<T> : IEcsRunSystem where T : struct
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<T>();
            
            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<T>();
            }
        }
    }
}
