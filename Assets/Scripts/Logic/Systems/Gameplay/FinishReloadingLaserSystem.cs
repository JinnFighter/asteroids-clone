using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class FinishReloadingLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                ref var laser = ref filter.Get1(index);
                laser.CurrentAmmo++;
                if (laser.CurrentAmmo < laser.MaxAmmo)
                {
                    ref var timer = ref filter.Get2(index);
                    timer.CurrentTime = 7f;
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new Counting());
                }
            }
        }
    }
}
