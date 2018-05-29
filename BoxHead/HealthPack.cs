using System;

class HealthPack : Item
{
    public int Health { get; }

    public HealthPack(short x, short y) 
        : base(new Image("img/items/healthpack.png", 60, 40), x, y)
    {
        Health = rdn.Next(50, 100);
    }
}
