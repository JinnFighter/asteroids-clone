using Logic.Events;
using Logic.Weapons;
using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Presenters;
using UnityScripts.Presentation.Views;

namespace UnityScripts.EventHandlers
{
    public class LaserEventHandler : IEventHandler<AmmoMagazine>
    {
        private LaserModel _laserModel;
        private readonly ILaserView _laserView;

        public LaserEventHandler(ILaserView laserView)
        {
            _laserView = laserView;
        }

        public void Handle(AmmoMagazine context)
        {
            _laserModel = new LaserModel(context.CurrentAmmo);
            context.AmmoChangedEvent += _laserModel.UpdateAmmoCount;
            var presenter = new LaserPresenter(_laserModel, _laserView);
        }
    }
}
