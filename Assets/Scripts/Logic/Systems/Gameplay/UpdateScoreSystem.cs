using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Containers;

namespace Logic.Systems.Gameplay
{
    public class UpdateScoreSystem : IEcsRunSystem
    {
        private readonly ScoreContainer _scoreContainer;

        public UpdateScoreSystem(ScoreContainer scoreContainer)
        {
            _scoreContainer = scoreContainer;
        }
        
        public void Run(EcsWorld ecsWorld)
        {
            var filter = ecsWorld.GetFilter<UpdateScoreEvent>();

            foreach (var index in filter)
            {
                ref var updateScoreEvent = ref filter.Get1(index);
                _scoreContainer.UpdateScore(updateScoreEvent.Score);
            }
        }
    }
}
