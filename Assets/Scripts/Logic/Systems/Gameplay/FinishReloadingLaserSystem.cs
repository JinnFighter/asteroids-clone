using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Events;

namespace Logic.Systems.Gameplay
{
    public class FinishReloadingLaserSystem : IEcsRunSystem
    {
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;

        public FinishReloadingLaserSystem(ComponentEventHandlerContainer componentEventHandlerContainer)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Laser, Timer, TimerEndEvent>();

            foreach (var index in filter)
            {
                ref var laser = ref filter.Get1(index);
                laser.CurrentAmmo++;
                var reloadEvent = new ReloadLaserEvent { CurrentAmmo = laser.CurrentAmmo };
                _componentEventHandlerContainer.HandleEvent(ref reloadEvent);
                
                var entity = filter.GetEntity(index);
                entity.AddComponent(reloadEvent);
                
                if (laser.CurrentAmmo < laser.MaxAmmo)
                {
                    ref var timer = ref filter.Get2(index);
                    timer.CurrentTime = 7f;
                    
                    entity.AddComponent(new Counting());
                }
            }
        }
    }
}
