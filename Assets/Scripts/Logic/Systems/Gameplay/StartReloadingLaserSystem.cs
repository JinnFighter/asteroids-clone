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
            var filter = ecsWorld.GetFilter<Laser, ShootLaserEvent, Timer>().Exclude<Counting>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.AddComponent(new Counting());
            }
        }
    }
}
