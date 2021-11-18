using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;
using Logic.Events;
using Logic.Weapons;

namespace Logic.Systems.Gameplay
{
    public class CreateLaserGunSystem : IEcsInitSystem
    {
        private readonly LaserConfig _laserConfig;
        private readonly AmmoMagazineHandlerKeeper _ammoMagazineHandlerKeeper;
        private readonly TimerHandlerKeeper _timerHandlerKeeper;

        public CreateLaserGunSystem(LaserConfig laserConfig, AmmoMagazineHandlerKeeper ammoMagazineHandlerKeeper,
            TimerHandlerKeeper timerHandlerKeeper)
        {
            _laserConfig = laserConfig;
            _ammoMagazineHandlerKeeper = ammoMagazineHandlerKeeper;
            _timerHandlerKeeper = timerHandlerKeeper;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var laserAmmoMagazine = new LaserMagazine(0, 3);
                _ammoMagazineHandlerKeeper.HandleEvent<LaserGun>(laserAmmoMagazine);
                var laser = new LaserGun { AmmoMagazine = laserAmmoMagazine };
                entity.AddComponent(laser);
                var gameplayTimer = new GameplayTimer { StartTime = _laserConfig.ReloadTime, CurrentTime = _laserConfig.ReloadTime };
                _timerHandlerKeeper.HandleEvent<LaserGun>(gameplayTimer);
                entity.AddComponent(new Timer{ GameplayTimer = gameplayTimer });
            }
        }
    }
}
