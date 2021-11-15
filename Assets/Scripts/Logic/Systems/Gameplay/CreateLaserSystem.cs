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
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;

        public CreateLaserSystem(ComponentEventHandlerContainer componentEventHandlerContainer)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            var filter = world.GetFilter<Ship>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                var laserAmmoMagazine = new LaserMagazine(0, 3);
                var laser = new Laser { CurrentAmmo = 0, MaxAmmo = 3, AmmoMagazine = laserAmmoMagazine };
                entity.AddComponent(laser);
                _componentEventHandlerContainer.HandleEvent(ref laser);
                entity.AddComponent(new Timer{ CurrentTime = 7f });
                entity.AddComponent(new Counting());
            }
        }
    }
}
