class Barrel : Obstacle
{
    public double Resistantce { get; }
    public double Damage { get; }

    public Barrel(short x, short y) 
        : base(new Image("imgs/obstacles/barrel.png", 36, 36), x, y)
    {
        Resistantce = 150;
        Damage = 75.5f;
    }
}
