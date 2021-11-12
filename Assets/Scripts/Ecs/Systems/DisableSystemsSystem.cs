using Ecs.Components;
using Ecs.Interfaces;

namespace Ecs.Systems
{
    public class DisableSystemsSystem : IEcsRunSystem
    {
        private readonly EcsSystems _systems;

        public DisableSystemsSystem(EcsSystems systems)
        {
            _systems = systems;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<DisableSystemsEvent>();

            foreach (var index in filter)
            {
                var disableEvent = filter.Get1(index);
                _systems.SetRunSystemState(disableEvent.Tag, false);
            }
        }
    }
}
