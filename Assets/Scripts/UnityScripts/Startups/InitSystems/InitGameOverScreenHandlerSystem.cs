using Ecs;
using Ecs.Interfaces;
using Logic.Containers;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Presentation.Screens;

namespace UnityScripts.Startups.InitSystems
{
    public class InitGameOverScreenHandlerSystem : IEcsInitSystem
    {
        private readonly ComponentEventHandlerContainer _componentEventHandlerContainer;
        private readonly GameOverScreen _gameOverScreen;
        private readonly ScoreContainer _scoreContainer;

        public InitGameOverScreenHandlerSystem(ComponentEventHandlerContainer componentEventHandlerContainer, 
            GameOverScreen gameOverScreen, ScoreContainer scoreContainer)
        {
            _componentEventHandlerContainer = componentEventHandlerContainer;
            _gameOverScreen = gameOverScreen;
            _scoreContainer = scoreContainer;
        }

        public void Init(EcsWorld world) =>
            _componentEventHandlerContainer.AddHandler(
                new ShowGameOverScreenEventHandler(_gameOverScreen, _scoreContainer));
    }
}
