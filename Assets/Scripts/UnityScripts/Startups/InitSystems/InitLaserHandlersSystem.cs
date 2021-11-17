using Ecs;
using Ecs.Interfaces;
using Logic.Events;
using UnityScripts.EventHandlers;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Startups.InitSystems
{
    public class InitLaserHandlersSystem : IEcsInitSystem
    {
        private readonly LaserMagazineHandlerContainer _laserMagazineHandlerContainer;
        private readonly ILaserView _laserView;

        public InitLaserHandlersSystem(LaserMagazineHandlerContainer laserMagazineHandlerContainer,
            ILaserView laserView)
        {
            _laserMagazineHandlerContainer = laserMagazineHandlerContainer;
            _laserView = laserView;
        }

        public void Init(EcsWorld world) =>
            _laserMagazineHandlerContainer.AddHandler(new LaserEventHandler(_laserView));
    }
}
