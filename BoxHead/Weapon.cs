abstract class Weapon : Sprite
{
    public Bullet Bullet { get; set; } 
    public int MaxAmountOfAmmo { get; }

    public Weapon(Bullet bullet, int maxAmountOfAmmo)
    {
        Bullet = bullet;
        MaxAmountOfAmmo = maxAmountOfAmmo;
    }
}
