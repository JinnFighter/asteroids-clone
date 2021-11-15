using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Events;

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
                var magazine = laser.AmmoMagazine;
                magazine.Reload();
            }
        }
    }
}
