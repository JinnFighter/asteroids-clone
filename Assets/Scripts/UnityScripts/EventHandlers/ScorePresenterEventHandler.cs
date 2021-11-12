using System.Collections.Generic;
using Logic.Containers;
using Logic.Events;
using UnityScripts.Factories;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class ScorePresenterEventHandler : IEventHandler<ScoreContainer>
    {
        private readonly IScorePresenterFactory _scorePresenterFactory;
        private readonly Dictionary<ScoreContainer, List<ScorePresenter>> _presenters;
        private readonly IScoreView _scoreView;

        public ScorePresenterEventHandler(IScorePresenterFactory scorePresenterFactory, IScoreView scoreView)
        {
            _scorePresenterFactory = scorePresenterFactory;
            _scoreView = scoreView;
            _presenters = new Dictionary<ScoreContainer, List<ScorePresenter>>();
        }
        
        public void OnCreateEvent(ScoreContainer context)
        {
            var presenter = _scorePresenterFactory.CreatePresenter(context, _scoreView);
            List<ScorePresenter> contextPresenters;
            if (_presenters.ContainsKey(context))
                contextPresenters = _presenters[context];
            else
            {
                contextPresenters = new List<ScorePresenter>();
                _presenters[context] = contextPresenters;
            }
            
            contextPresenters.Add(presenter);
        }

        public void OnDestroyEvent(ScoreContainer context)
        {
            foreach (var presenter in _presenters[context])
                presenter.Destroy();
            
            _presenters[context].Clear();
        }
    }
}
