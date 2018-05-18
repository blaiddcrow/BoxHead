class Mine : Obstacle
{
    protected Image mine;
    public double Damage { get; }

    public Mine(short x, short y) : base(x, y)
    {
        mine = new Image("img/barrel.png", 36, 36); // Aux image.
        Damage = 80;
    }
}
