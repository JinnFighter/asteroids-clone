using Logic.Containers;
using Logic.Events;
using UnityScripts.Factories;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ScorePresenterEventHandler : IEventHandler<ScoreContainer>
    {
        private readonly IScorePresenterFactory _scorePresenterFactory;
        private readonly IScoreView _scoreView;

        public ScorePresenterEventHandler(IScorePresenterFactory scorePresenterFactory, IScoreView scoreView)
        {
            _scorePresenterFactory = scorePresenterFactory;
            _scoreView = scoreView;
        }
        
        public void Handle(ScoreContainer context)
        {
            var presenter = _scorePresenterFactory.CreatePresenter(context, _scoreView);
        }
    }
}
