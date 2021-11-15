using Logic.Components.Gameplay;
using Logic.Events;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class LaserEventHandler : IComponentEventHandler<ShootLaserEvent>, IComponentEventHandler<Laser>
    {
        private LaserModel _laserModel;
        private LaserPresenter _laserPresenter;
        private readonly ILaserView _laserView;

        public LaserEventHandler(ILaserView laserView)
        {
            _laserView = laserView;
        }
        
        public void Handle(ref ShootLaserEvent context) => _laserModel.UpdateAmmoCount(context.CurrentAmmo);

        public void Handle(ref Laser context)
        {
            _laserModel = new LaserModel(context.CurrentAmmo);
            _laserPresenter = new LaserPresenter(_laserModel, _laserView);
        }
    }
}
