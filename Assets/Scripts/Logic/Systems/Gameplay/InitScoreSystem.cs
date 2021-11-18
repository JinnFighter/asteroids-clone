using Ecs;
using Ecs.Interfaces;
using Logic.Containers;
using Logic.Services;

namespace Logic.Systems.Gameplay
{
    public class InitScoreSystem : IEcsInitSystem
    {
        private readonly ScoreContainer _scoreContainer;
        private readonly ScoreEventHandlerContainer _scoreEventHandlerContainer;

        public InitScoreSystem(ScoreContainer scoreContainer, ScoreEventHandlerContainer scoreEventHandlerContainer)
        {
            _scoreContainer = scoreContainer;
            _scoreEventHandlerContainer = scoreEventHandlerContainer;
        }
        
        public void Init(EcsWorld world)
        {
            _scoreContainer.UpdateScore(0);
            _scoreEventHandlerContainer.HandleEvent(_scoreContainer);
        }
    }
}
