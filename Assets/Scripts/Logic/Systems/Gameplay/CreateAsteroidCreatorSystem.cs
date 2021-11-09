using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Services;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidCreatorSystem : IEcsInitSystem
    {
        private readonly IRandomizer _random;

        public CreateAsteroidCreatorSystem(IRandomizer random)
        {
            _random = random;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            var asteroidCreatorConfig = new AsteroidCreatorConfig { MinTime = 1, MaxTime = 3 };
            entity.AddComponent(asteroidCreatorConfig);
            
            var timer = new Timer
                { CurrentTime = _random.Range(asteroidCreatorConfig.MinTime, asteroidCreatorConfig.MaxTime) };
            entity.AddComponent(timer);
            entity.AddComponent(new Counting());
        }
    }
}
