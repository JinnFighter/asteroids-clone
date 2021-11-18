using Logic.Components.Gameplay;
using Logic.Containers;
using Logic.Events;
using UnityScripts.Presentation.Screens;

namespace UnityScripts.EventHandlers
{
    public class ShowGameOverScreenEventHandler : IComponentEventHandler<GameOverEvent>
    {
        private readonly GameOverScreen _gameOverScreen;
        private readonly ScoreContainer _scoreContainer;

        public ShowGameOverScreenEventHandler(GameOverScreen gameOverScreen, ScoreContainer scoreContainer)
        {
            _gameOverScreen = gameOverScreen;
            _scoreContainer = scoreContainer;
        }
        
        public void Handle(ref GameOverEvent context)
        {
            _gameOverScreen.UpdateScore(_scoreContainer.Score);
            _gameOverScreen.gameObject.SetActive(true);
        }
    }
}
