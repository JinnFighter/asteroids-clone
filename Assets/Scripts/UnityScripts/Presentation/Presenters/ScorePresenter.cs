using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class ScorePresenter
    {
        private readonly ScoreModel _scoreModel;
        private readonly IScoreView _scoreView;

        public ScorePresenter(ScoreModel scoreModel, IScoreView scoreView)
        {
            _scoreModel = scoreModel;
            _scoreModel.ScoreChangedEvent += UpdateScore;
            _scoreView = scoreView;
        }
        
        private void UpdateScore(int score) => _scoreView.UpdateScore(score);

        public void Destroy()
        {
            _scoreModel.ScoreChangedEvent -= UpdateScore;
        }
    }
}
