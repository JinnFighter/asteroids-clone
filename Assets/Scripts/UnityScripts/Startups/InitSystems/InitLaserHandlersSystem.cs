using Ecs;
using Ecs.Interfaces;
using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserHandlersSystem : IEcsInitSystem
    {
        private readonly AmmoMagazineHandlerKeeper _ammoMagazineHandlerKeeper;
        private readonly ILaserView _laserView;

        public InitLaserHandlersSystem(AmmoMagazineHandlerKeeper ammoMagazineHandlerKeeper,
            ILaserView laserView)
        {
            _ammoMagazineHandlerKeeper = ammoMagazineHandlerKeeper;
            _laserView = laserView;
        }

        public void Init(EcsWorld world) =>
            _ammoMagazineHandlerKeeper.AddHandler<LaserGun>(new LaserEventHandler(_laserView));
    }
}
