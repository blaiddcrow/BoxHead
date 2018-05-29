class Wall : Obstacle
{
    public Wall(short x, short y) 
        : base(new Image("imgs/obstacles/wall.png", 40, 40), x, y)
    {
    }

    public Wall(Image image, short x, short y) : base(image, x, y)
    {
    }


    public override string ToString()
    {
        return "w;" + base.ToString();
    }
}
