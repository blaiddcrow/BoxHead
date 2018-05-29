/*
* V.0.0.7 22/05/2018 - Javier Saorín Vidal: Added an image for the spawnpoints.
*/
class SpawnPoint : Obstacle
{
    public SpawnPoint(short x, short y) :
        base(new Image("imgs/obstacles/spawnpoint.png", 50, 50), x, y)
    {
    }

    public SpawnPoint(Image image, short x, short y) : base(image, x, y)
    {
    }

    public override string ToString()
    {
        return "s;" + base.ToString();
    }
}
