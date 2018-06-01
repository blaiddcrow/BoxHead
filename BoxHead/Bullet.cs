
class Bullet : MovableSprite 
{
    public double Damage { get; set; }
    public short XDirection { get; set; }
    public short YDirection { get; set; }

    public Bullet(short xDirection, short yDirection)
    {
        Damage = 10;
        Speed = 1;
        XDirection = xDirection;
        YDirection = yDirection;
        Image = new Image("imgs/effects/bullet.png", 15, 15);
    }
}
