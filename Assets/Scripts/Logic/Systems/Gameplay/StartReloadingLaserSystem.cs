using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class StartReloadingLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, Timer>().Exclude<Counting>();

            foreach (var index in filter)
            {
                var laser = filter.Get1(index);
                var magazine = laser.AmmoMagazine;
                if (magazine.CurrentAmmo < magazine.MaxAmmo)
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
