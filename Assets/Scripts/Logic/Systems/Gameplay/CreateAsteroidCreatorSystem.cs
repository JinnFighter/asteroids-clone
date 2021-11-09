using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidCreatorSystem : IEcsInitSystem
    {
        private readonly IRandomizer _randomizer;

        public CreateAsteroidCreatorSystem(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            var asteroidCreatorConfig = new AsteroidCreatorConfig { MinTime = 1, MaxTime = 3 };
            entity.AddComponent(asteroidCreatorConfig);
            
            var timer = new Timer
                { CurrentTime = _randomizer.Range(asteroidCreatorConfig.MinTime, asteroidCreatorConfig.MaxTime) };
            entity.AddComponent(timer);
            entity.AddComponent(new Counting());
        }
    }
}
