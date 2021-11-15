using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Events;
using Logic.Weapons;

namespace Logic.Systems.Gameplay
{
    public class CreateLaserSystem : IEcsInitSystem
    {
        private readonly LaserMagazineHandlerContainer _laserMagazineHandlerContainer;

        public CreateLaserSystem(LaserMagazineHandlerContainer laserMagazineHandlerContainer)
        {
            _laserMagazineHandlerContainer = laserMagazineHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var laserAmmoMagazine = new LaserMagazine(0, 3);
                _laserMagazineHandlerContainer.OnCreateEvent(laserAmmoMagazine);
                var laser = new Laser { AmmoMagazine = laserAmmoMagazine };
                entity.AddComponent(laser);
                entity.AddComponent(new Timer());
            }
        }
    }
}
