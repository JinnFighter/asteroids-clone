using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class CreateDestroyBulletEventSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Bullet, Timer, TimerEndEvent>().Exclude<DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.AddComponent(new DestroyEvent());
            }
        }
    }
}
