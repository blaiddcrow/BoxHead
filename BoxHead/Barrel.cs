class Barrel : Obstacle
{
    public double Resistance { get; }
    public double Damage { get; }

    public Barrel(short x, short y) 
        : base(new Image("imgs/obstacles/barrel.png", 36, 36), x, y)
    {
        Resistance = 150;
        Damage = 75.5f;
    }

    public Barrel(Image image, short x, short y) : base(image, x, y)
    {
    }

    public override string ToString()
    {
        return "b;" + base.ToString();
    }
}
