using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Events;
using Logic.Weapons;

namespace Logic.Systems.Gameplay
{
    public class CreateLaserSystem : IEcsInitSystem
    {
        private readonly LaserMagazineHandlerContainer _laserMagazineHandlerContainer;
        private readonly LaserTimerHandlerContainer _laserTimerHandlerContainer;

        public CreateLaserSystem(LaserMagazineHandlerContainer laserMagazineHandlerContainer, 
            LaserTimerHandlerContainer laserTimerHandlerContainer)
        {
            _laserMagazineHandlerContainer = laserMagazineHandlerContainer;
            _laserTimerHandlerContainer = laserTimerHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var laserAmmoMagazine = new LaserMagazine(0, 3);
                _laserMagazineHandlerContainer.OnCreateEvent(laserAmmoMagazine);
                var laser = new LaserGun { AmmoMagazine = laserAmmoMagazine };
                entity.AddComponent(laser);
                var gameplayTimer = new GameplayTimer { StartTime = 7f, CurrentTime = 7f };
                _laserTimerHandlerContainer.OnCreateEvent(gameplayTimer);
                entity.AddComponent(new Timer{ GameplayTimer = gameplayTimer });
            }
        }
    }
}
