using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidSystem : IEcsRunSystem
    {
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<CreateAsteroidEvent>();

            foreach (var index in filter)
            {
                var entity = ecsWorld.CreateEntity();
                entity.AddComponent(new Asteroid());
            }
        }
    }
}
