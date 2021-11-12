using Ecs.Components;
using Ecs.Interfaces;

namespace Ecs.Systems
{
    internal class EnableSystemsSystem : IEcsRunSystem
    {
        private readonly EcsSystems _systems;

        public EnableSystemsSystem(EcsSystems systems)
        {
            _systems = systems;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<EnableSystemsEvent>();

            foreach (var index in filter)
            {
                var enableEvent = filter.Get1(index);
                _systems.SetRunSystemState(enableEvent.Tag, true);
            }
        }
    }
}
