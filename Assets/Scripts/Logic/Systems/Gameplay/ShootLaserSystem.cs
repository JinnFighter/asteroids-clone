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
            var filter = ecsWorld.GetFilter<LaserGun, LaserFireInputAction>();

            foreach (var index in filter)
            {
                ref var laser = ref filter.Get1(index);
                var magazine = laser.AmmoMagazine;
                if (magazine.CurrentAmmo > 0)
                {
                    magazine.Shoot();
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new ShootEvent());
                }
            }
        }
    }
}
