using Logic.Containers;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public class ScorePresenterFactory : IScorePresenterFactory
    {
        public ScorePresenter CreatePresenter(ScoreContainer scoreContainer, IScoreView scoreView)
        {
            var scoreModel = new ScoreModel();
            scoreContainer.ScoreChangedEvent += scoreModel.UpdateScore;

            return new ScorePresenter(scoreModel, scoreView);
        }
    }
}
