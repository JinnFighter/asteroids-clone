using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Physics;
using Logic.Conveyors;
using Physics;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidSystem : IEcsRunSystem
    {
        private readonly AsteroidCreatorConveyor _asteroidCreatorConveyor;

        public CreateAsteroidSystem(AsteroidCreatorConveyor conveyor)
        {
            _asteroidCreatorConveyor = conveyor;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateAsteroidEvent>();

            foreach (var index in filter)
            {
                ref var createAsteroidEvent = ref filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                _asteroidCreatorConveyor.UpdateItem(entity, createAsteroidEvent);
            }
        }
    }
}
