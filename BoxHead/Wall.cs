class Wall : Obstacle
{
    public Wall(short x, short y) 
        : base(new Image("imgs/obstacles/wall.png", 40, 40), x, y)
    {
    }
}
