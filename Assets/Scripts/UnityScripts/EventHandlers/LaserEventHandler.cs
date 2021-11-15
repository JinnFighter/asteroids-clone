using Logic.Events;
using Logic.Weapons;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class LaserEventHandler : IEventHandler<LaserMagazine>
    {
        private LaserModel _laserModel;
        private LaserPresenter _laserPresenter;
        private readonly ILaserView _laserView;

        public LaserEventHandler(ILaserView laserView)
        {
            _laserView = laserView;
        }

        public void Handle(LaserMagazine context)
        {
            _laserModel = new LaserModel(context.CurrentAmmo);
            context.AmmoChangedEvent += _laserModel.UpdateAmmoCount;
            _laserPresenter = new LaserPresenter(_laserModel, _laserView);
        }
    }
}
