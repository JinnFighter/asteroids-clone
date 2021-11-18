using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class StartReloadingLaserSystem : IEcsRunSystem
    {
        private readonly LaserConfig _laserConfig;

        public StartReloadingLaserSystem(LaserConfig laserConfig)
        {
            _laserConfig = laserConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<LaserGun, Timer>().Exclude<Counting>();

            foreach (var index in filter)
            {
                var laser = filter.Get1(index);
                var magazine = laser.AmmoMagazine;
                if (magazine.CurrentAmmo < magazine.MaxAmmo)
                {
                    var timer = filter.Get2(index);
                    var gameplayTimer = timer.GameplayTimer;
                    var reloadTime = _laserConfig.ReloadTime;
                    gameplayTimer.StartTime = reloadTime;
                    gameplayTimer.CurrentTime = reloadTime;
                    var entity = filter.GetEntity(index);
                    entity.AddComponent(new Counting());
                }
            }
        }
    }
}
