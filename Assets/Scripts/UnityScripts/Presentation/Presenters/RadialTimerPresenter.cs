using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
    public class RadialTimerPresenter : TimerPresenter
    {
        public RadialTimerPresenter(TimerModel timerModel, ITimerView timerView) : base(timerModel, timerView)
        {
        }

        protected override void UpdateTime(float time) => TimerView.UpdateCurrentTime((TimerModel.StartTime - time) / TimerModel.StartTime);
    }
}
