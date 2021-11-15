namespace Logic.Weapons
{
    public abstract class AmmoMagazine : IAmmoMagazine
    {
        public int CurrentAmmo { get; protected set; }
        public int MaxAmmo { get; }

        public AmmoMagazine(int currentAmmo, int maxAmmo)
        {
            CurrentAmmo = currentAmmo;
            MaxAmmo = maxAmmo;
        }

        public delegate void AmmoChanged(int currentAmmo);

        public event AmmoChanged AmmoChangedEvent;
        
        protected abstract void ShootInternal();
        
        public void Shoot()
        {
            if (CurrentAmmo > 0)
            {
                ShootInternal();
                AmmoChangedEvent?.Invoke(CurrentAmmo);
            }
        }

        protected abstract void ReloadInternal();
        
        public void Reload()
        {
            if (CurrentAmmo < MaxAmmo)
            {
                ReloadInternal();
                AmmoChangedEvent?.Invoke(CurrentAmmo);
            }
        }
    }
}
