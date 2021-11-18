using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class DestroyBulletsSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Bullet, Timer, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Bullet>();
                entity.RemoveComponent<Timer>();
                if(entity.HasComponent<Counting>())
                    entity.RemoveComponent<Counting>();
            }
        }
    }
}
