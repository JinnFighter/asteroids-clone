using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Input;
using Logic.Events;

namespace Logic.Systems.Gameplay
{
    public class ShootLaserSystem : IEcsRunSystem
    {
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;

        public ShootLaserSystem(ComponentEventHandlerContainer componentEventHandlerContainer)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, LaserFireInputAction>();

            foreach (var index in filter)
            {
                ref var laser = ref filter.Get1(index);
                if (laser.CurrentAmmo > 0)
                {
                    laser.CurrentAmmo--;
                    var entity = filter.GetEntity(index);
                    var shootLaserEvent = new ShootLaserEvent { CurrentAmmo = laser.CurrentAmmo };
                    _componentEventHandlerContainer.HandleEvent(ref shootLaserEvent);
                    entity.AddComponent(shootLaserEvent);
                }
            }
        }
    }
}
