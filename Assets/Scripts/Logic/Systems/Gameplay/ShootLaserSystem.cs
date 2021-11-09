using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;

namespace Logic.Systems.Gameplay
{
    public class ShootLaserSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, LaserFireInputAction>();

            foreach (var index in filter)
            {
                ref var laser = ref filter.Get1(index);
                if (laser.CurrentAmmo > 0)
                {
                    laser.CurrentAmmo--;
                }
            }
        }
    }
}
