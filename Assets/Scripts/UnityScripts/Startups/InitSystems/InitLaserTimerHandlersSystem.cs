using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserTimerHandlersSystem : IEcsInitSystem
    {
        private readonly TimerHandlerKeeper _timerHandlerKeeper;
        private readonly ITimerView _timerView;

        public InitLaserTimerHandlersSystem(TimerHandlerKeeper timerHandlerKeeper, ITimerView timerView)
        {
            _timerHandlerKeeper = timerHandlerKeeper;
            _timerView = timerView;
        }

        public void Init(EcsWorld world) => 
            _timerHandlerKeeper.AddHandler<LaserGun>(new TimerPresenterHandler(_timerView));
    }
}
