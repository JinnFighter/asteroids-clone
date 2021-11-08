using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Conveyors;

namespace Logic.Systems.Gameplay
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly BulletCreatorConveyor _asteroidCreatorConveyor;

        public SpawnBulletSystem(BulletCreatorConveyor conveyor)
        {
            _asteroidCreatorConveyor = conveyor;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateBulletEvent>();

            foreach (var index in filter)
            {
                ref var createBulletEvent = ref filter.Get1(index);
                var entity = ecsWorld.CreateEntity();
                _asteroidCreatorConveyor.UpdateItem(entity, createBulletEvent);
            }
        }
    }
}
