using Ecs;
using Ecs.Interfaces;
using Logic.Services;
using UnityScripts.EventHandlers;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitScoreHandlersSystem : IEcsInitSystem
    {
        private readonly ScoreEventHandlerContainer _scoreEventHandlerContainer;
        private readonly IScoreView _scoreView;

        public InitScoreHandlersSystem(ScoreEventHandlerContainer scoreEventHandlerContainer, IScoreView scoreView)
        {
            _scoreEventHandlerContainer = scoreEventHandlerContainer;
            _scoreView = scoreView;
        }
        
        public void Init(EcsWorld world) 
            => _scoreEventHandlerContainer.AddHandler(new ScorePresenterEventHandler(new ScorePresenterFactory(), _scoreView));
    }
}
