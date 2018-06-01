
using System;
using System.Collections.Generic;

class Character : MovableSprite
{
    public double Life { get; set; }
    public bool IsAlive { get; set; }
    public List<Weapon> weapons { get; }
    public Pistol Glock { get; set; }
    public int ActualWeapon { get; set; }
    public int AmountOfgrenades { get; set; }
    public int Points { get; set; }
    public int ActualImage { get; set; }
    public Image Image { get; set; }
    public Image[] Pistol;
    public short XMap { get; set; }
    public short YMap { get; set; }
    public byte MovementDirection { get; set; }

    public Character(double life)
    {
        Life = life;
        Speed = 2;
        IsAlive = true;
        weapons = new List<Weapon>();
        Glock = new Pistol(15);
        weapons.Add(Glock);
        ActualWeapon = 0;
        Pistol = new Image[36];
        initialiceImages();
        ActualImage = 0;
        AmountOfgrenades = 3;
        Image = Pistol[ActualImage]; // TODO: Change between weapons.
        Points = 0;
        MovementDirection = 0;
    }

    private void initialiceImages()
    {
        int imageDegrees = 0;

        for (int i = 0; i < Pistol.Length; i++)
        {
            Pistol[i] = new Image("imgs/character/pistol/pistol" + 
                imageDegrees + ".png", 60, 60);
            imageDegrees += 10;
        }

        foreach (Image image in Pistol)
        {
            image.X = 
                (short)((GameController.SCREEN_WIDTH / 2) - (Width / 2));
            image.Y = 
                (short)((GameController.SCREEN_HEIGHT / 2) - (Height / 2));
        }
    }

    private bool isBetween(double number, int min, int max)
    {
        return number > min && number < max;
    }

    public void Animate(int mouseX, int mouseY)
    {
        int centerX = GameController.SCREEN_WIDTH / 2;
        int centerY = GameController.SCREEN_HEIGHT / 2;

        double deg = 
            Math.Atan2(centerY - mouseY, centerX - mouseX) * 180 / Math.PI + 180;

        for (int i = 0; i < 36; i++)
            if (isBetween(deg, i * 10, (i + 1) * 10))
                ActualImage = i;

        Image = Pistol[ActualImage];
    }

    public bool CollidesWith(Obstacle o)
    {
        return (X + Image.ImageWidth > o.X
            && X < o.X + Image.ImageWidth
            && Y + Image.ImageHeight > o.Y
            && Y < o.Y + Image.ImageHeight);
    }

    public bool CheckIfCollides(Level level)
    {
        bool hasCollided = false;
        for (int i = 0; i < level.Obstacles.Count && !hasCollided; i++)    
            hasCollided = CollidesWith(level.Obstacles[i]);
        return hasCollided;
    }

    public void Shoot(short mouseX, short mouseY, ref DateTime timeStampFromLastShot)
    {
        if ((DateTime.Now - timeStampFromLastShot).TotalMilliseconds
            > Glock.ShootInterval)
        {
            timeStampFromLastShot = DateTime.Now;
            AddBullet(mouseX, mouseY);
        }
    }

    public void AddBullet(short mouseX, short mouseY)
    {
        Bullet newBullet = new Bullet(mouseX, mouseX);
        newBullet.X = 
            (short)(GameController.SCREEN_WIDTH / 2 - newBullet.Width / 2);
        newBullet.Y = 
            (short)(GameController.SCREEN_HEIGHT / 2 - newBullet.Height / 2);
        newBullet.CurrentDirection = this.CurrentDirection;
        newBullet.UpdateSpriteCoordinates();
        weapons[ActualWeapon].Bullets.Add(newBullet);
    }

    public void RemoveBullet(int index)
    {
        weapons[ActualWeapon].Bullets.RemoveAt(index);
    }
}