using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserTimerHandlersSystem : IEcsInitSystem
    {
        private readonly LaserTimerHandlerContainer _laserTimerHandlerContainer;
        private readonly ITimerView _timerView;

        public InitLaserTimerHandlersSystem(LaserTimerHandlerContainer laserTimerHandlerContainer,
            ITimerView timerView)
        {
            _laserTimerHandlerContainer = laserTimerHandlerContainer;
            _timerView = timerView;
        }

        public void Init(EcsWorld world) => 
            _laserTimerHandlerContainer.AddHandler(new TimerPresenterHandler(_timerView));
    }
}
