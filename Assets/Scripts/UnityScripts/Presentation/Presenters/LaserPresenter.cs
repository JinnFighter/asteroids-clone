using UnityScripts.Presentation.Models;
using UnityScripts.Presentation.Views;

namespace UnityScripts.Presentation.Presenters
{
   public class LaserPresenter
   {
      private readonly LaserModel _laserModel;
      private readonly ILaserView _laserView;

      public LaserPresenter(LaserModel laserModel, ILaserView laserView)
      {
         _laserModel = laserModel;
         _laserModel.AmmoCountChangedEvent += UpdateAmmoCount;
         _laserView = laserView;
      }

      private void UpdateAmmoCount(int ammoCount)
      {
         if(ammoCount >= 0)
            _laserView.UpdateAmmoCount(ammoCount);
      }

      public void Destroy()
      {
         _laserModel.AmmoCountChangedEvent -= UpdateAmmoCount;
      }
   }
}
