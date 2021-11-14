using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;

namespace Logic.Systems.Gameplay
{
    public class DestroySaucerSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Saucer, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Saucer>();
            }
        }
    }
}
