using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;

namespace Logic.Systems.Gameplay
{
    public class DestroyShipsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Ship, DestroyEvent>();
            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Ship>();
            }
        }
    }
}
