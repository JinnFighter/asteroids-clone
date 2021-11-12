using Ecs;
using Ecs.Interfaces;
using Logic.Containers;
using Logic.Services;

namespace Logic.Systems.Gameplay
{
    public class InitScoreSystem : IEcsInitSystem
    {
        private readonly ScoreContainer _scoreContainer;
        private readonly ScoreEventListener _scoreEventListener;

        public InitScoreSystem(ScoreContainer scoreContainer, ScoreEventListener scoreEventListener)
        {
            _scoreContainer = scoreContainer;
            _scoreEventListener = scoreEventListener;
        }
        
        public void Init(EcsWorld world)
        {
            _scoreContainer.UpdateScore(0);
            _scoreEventListener.OnCreateEvent(_scoreContainer);
        }
    }
}
