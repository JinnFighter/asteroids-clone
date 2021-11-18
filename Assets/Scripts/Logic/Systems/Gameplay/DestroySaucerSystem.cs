using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Config;

namespace Logic.Systems.Gameplay
{
    public class DestroySaucerSystem : IEcsRunSystem
    {
        private readonly ScoreConfig _scoreConfig;

        public DestroySaucerSystem(ScoreConfig scoreConfig)
        {
            _scoreConfig = scoreConfig;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<Saucer, DestroyEvent>();

            foreach (var index in filter)
            {
                var entity = filter.GetEntity(index);
                entity.RemoveComponent<Saucer>();

                entity.AddComponent(new UpdateScoreEvent { Score = _scoreConfig.SaucerScore });
            }
        }
    }
}
