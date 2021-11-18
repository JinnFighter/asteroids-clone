using Helpers;
using Logic.Events;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class TimerPresenterHandler : IEventHandler<GameplayTimer>
    {
        private readonly ITimerView _timerView;

        public TimerPresenterHandler(ITimerView timerView)
        {
            _timerView = timerView;
        }
        
        public void Handle(GameplayTimer context)
        {
            var timerModel = new TimerModel(context.StartTime);
            context.StartTimeChangedEvent += timerModel.UpdateStartTime;
            context.CurrentTimeChangedEvent += timerModel.UpdateTime;

            var presenter = new RadialTimerPresenter(timerModel, _timerView);
        }
    }
}
