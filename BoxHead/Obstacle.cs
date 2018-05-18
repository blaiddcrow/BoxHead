class Obstacle : StaticSprite
{
    public Image Image { get; }
    public Obstacle(Image image, short x, short y)
    {
        Image = image;
        X = x;
        Y = y;
    }

    public Obstacle(short x, short y)
    {
        X = x;
        Y = y;
    }
}
