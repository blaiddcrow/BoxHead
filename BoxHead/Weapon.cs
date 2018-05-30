abstract class Weapon : StaticSprite
{
    public Bullet Bullet { get; set; }
    public int Ammo { get; set; }
    public int MaxAmountOfAmmo { get; }

    public Weapon(Bullet bullet, int maxAmountOfAmmo)
    {
        Bullet = bullet;
        MaxAmountOfAmmo = maxAmountOfAmmo;
        Ammo = maxAmountOfAmmo;
    }
}
