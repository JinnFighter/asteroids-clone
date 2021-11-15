using Logic.Weapons;

namespace Logic.Components.Gameplay
{
    public struct Laser
    {
        public int CurrentAmmo;
        public int MaxAmmo;
        public IAmmoMagazine AmmoMagazine;
    }
}
