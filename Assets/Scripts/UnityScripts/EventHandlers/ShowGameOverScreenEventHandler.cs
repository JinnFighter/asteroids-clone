using Logic.Components.Gameplay;
using Logic.Containers;
using Logic.Events;
using UnityScripts.Presentation.Screens;

namespace UnityScripts.EventHandlers
{
    public class GameOverScreenEventHandler : IComponentEventHandler<GameOverEvent>
    {
        private readonly GameOverScreen _gameOverScreen;
        private readonly ScoreContainer _scoreContainer;

        public GameOverScreenEventHandler(GameOverScreen gameOverScreen, ScoreContainer scoreContainer)
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
