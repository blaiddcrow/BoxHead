
using System;
using System.Collections.Generic;

class Character : MovableSprite
{
    public double Life { get; set; }
    public bool IsAlive { get; set; }
    public List<Weapon> weapons { get; }
    public int Points { get; set; }
    public int ActualImage { get; set; }
    public Image Image { get; set; }
    public Image[] Pistol;
    public short XMap { get; set; }
    public short YMap { get; set; }

    public Character(double life)
    {
        Life = life;
        Speed = 2;
        IsAlive = true;
        weapons = new List<Weapon>();
        Pistol = new Image[36];
        initialiceImages();
        ActualImage = 0;
        Image = Pistol[ActualImage]; // TODO: Change between weapons.
        Points = 0;
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

    public void Shoot(int mouseX, int mouseY)
    {
        // TODO: Make the logic of the shoots.
    }
}