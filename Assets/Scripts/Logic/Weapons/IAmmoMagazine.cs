namespace Logic.Weapons
{
    public interface IAmmoMagazine
    {
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
        void Shoot();
        void Reload();
    }
}
