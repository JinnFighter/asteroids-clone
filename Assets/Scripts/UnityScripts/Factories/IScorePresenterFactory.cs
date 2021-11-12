using Logic.Containers;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Factories
{
    public interface IScorePresenterFactory
    {
        ScorePresenter CreatePresenter(ScoreContainer scoreContainer, IScoreView scoreView);
    }
}
