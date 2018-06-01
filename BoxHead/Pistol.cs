class Pistol : Weapon
{
    public Pistol(int maxAmountOfAmmo) 
        : base(maxAmountOfAmmo)
    {
        ShootInterval = 200;
    }
}
