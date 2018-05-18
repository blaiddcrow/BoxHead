using System;

class HealthPack : Item
{
    public int Health { get; }

    public HealthPack()
    {
        Random r = new Random();
        Health = r.Next(50, 100);
    }
}
