using Ecs;
using Ecs.Interfaces;
using Helpers;
using Logic.Components.Gameplay;
using Logic.Components.Time;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class InitSaucerSpawnerSystem : IEcsInitSystem
    {
        private readonly SaucerConfig _saucerConfig;
        private readonly IRandomizer _randomizer;

        public InitSaucerSpawnerSystem(SaucerConfig saucerConfig, IRandomizer randomizer)
        {
            _saucerConfig = saucerConfig;
            _randomizer = randomizer;
        }
        
        public void Init(EcsWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComponent(new SaucerSpawnerConfig{ MinTime = _saucerConfig.MinTime, MaxTime = _saucerConfig.MaxTime });
            
            entity.AddComponent(new Timer{ CurrentTime = _randomizer.Range(_saucerConfig.MinTime, _saucerConfig.MaxTime) });
            entity.AddComponent(new Counting());
        }
    }
}
