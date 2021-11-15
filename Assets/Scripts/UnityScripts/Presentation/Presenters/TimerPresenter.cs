using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public abstract class TimerPresenter
    {
        private readonly TimerModel _timerModel;
        private readonly ITimerView _timerView;

        public TimerPresenter(TimerModel timerModel, ITimerView timerView)
        {
            _timerModel = timerModel;
            _timerModel.CurrentTimeChangedEvent += UpdateTime;
            _timerView = timerView;
        }

        protected abstract void UpdateTime(float time);
    }
}
