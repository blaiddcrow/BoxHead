class Enemy : MovableSprite
{
    public double Life { get; set; }
    public double Damage { get; set; }
    public bool IsAlive { get; set; }

    public Enemy(double life, double damage)
    {
        Life = life;
        Damage = damage;
        IsAlive = true;
    }
}
