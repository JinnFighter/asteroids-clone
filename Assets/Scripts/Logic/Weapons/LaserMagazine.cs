namespace Logic.Weapons
{
    public class LaserMagazine : AmmoMagazine
    {
        public LaserMagazine(int currentAmmo, int maxAmmo) : base(currentAmmo, maxAmmo)
        {
        }

        protected override void ShootInternal() => CurrentAmmo--;

        protected override void ReloadInternal() => CurrentAmmo++;
    }
}
