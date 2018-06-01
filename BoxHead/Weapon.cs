using System.Collections.Generic;

abstract class Weapon : StaticSprite
{
    public int Ammo { get; set; }
    public int MaxAmountOfAmmo { get; }
    public short ShootInterval { get; set; }
    public List<Bullet> Bullets { get; set; }

    public Weapon(int maxAmountOfAmmo)
    {
        Bullets = new List<Bullet>();
        MaxAmountOfAmmo = maxAmountOfAmmo;
        Ammo = maxAmountOfAmmo;
    }
}
