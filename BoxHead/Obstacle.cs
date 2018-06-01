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

    public override string ToString()
    {
        return X + ";" + Y;
    }

    public char GetTypeOfObstacle()
    {
        return char.Parse(ToString().Split(';')[0]);
    }
}
