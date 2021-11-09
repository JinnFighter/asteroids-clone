namespace UnityScripts.Presentation.Models
{
    public class LaserModel
    {
        public int AmmoCount { get; private set; }

        public delegate void AmmoCountChanged(int currentAmmo);

        public event AmmoCountChanged AmmoCountChangedEvent;

        public LaserModel(int ammoCount)
        {
            AmmoCount = ammoCount;
        }

        public void UpdateAmmoCount(int ammoCount)
        {
            AmmoCount = ammoCount;
            AmmoCountChangedEvent?.Invoke(ammoCount);
        }
    }
}
