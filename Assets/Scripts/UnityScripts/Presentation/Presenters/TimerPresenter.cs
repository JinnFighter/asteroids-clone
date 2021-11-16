using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public abstract class TimerPresenter
    {
        protected readonly TimerModel TimerModel;
        protected readonly ITimerView TimerView;

        public TimerPresenter(TimerModel timerModel, ITimerView timerView)
        {
            TimerModel = timerModel;
            TimerModel.CurrentTimeChangedEvent += UpdateTime;
            TimerView = timerView;
        }

        protected abstract void UpdateTime(float time);
    }
}
