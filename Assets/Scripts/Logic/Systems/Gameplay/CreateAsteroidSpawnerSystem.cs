using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class CreateAsteroidSpawnerSystem : IEcsInitSystem
    {
        private readonly IRandomizer _randomizer;
        private readonly AsteroidConfig _asteroidConfig;

        public CreateAsteroidSpawnerSystem(IRandomizer randomizer, AsteroidConfig asteroidConfig)
        {
            _randomizer = randomizer;
            _asteroidConfig = asteroidConfig;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            var asteroidCreatorConfig = new AsteroidCreatorConfig();
            entity.AddComponent(asteroidCreatorConfig);

            var time = _randomizer.Range(_asteroidConfig.MinRespawnTime, _asteroidConfig.MaxRespawnTime);
            var gameplayTimer = new GameplayTimer
            {
                StartTime = time,
                CurrentTime = time
            };
            
            var timer = new Timer { GameplayTimer = gameplayTimer };
            entity.AddComponent(timer);
            entity.AddComponent(new Counting());
        }
    }
}
