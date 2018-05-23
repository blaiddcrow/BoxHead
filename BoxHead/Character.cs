
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
        Speed = 1;
        IsAlive = true;
        weapons = new List<Weapon>();
        Pistol = new Image[8];
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
                imageDegrees + ".png", 100, 100);
            imageDegrees += 45;
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
        Console.WriteLine(deg);

        if (isBetween(deg, 337, 359) || isBetween(deg, 0, 23))
            ActualImage = 0;
        else if (isBetween(deg, 22, 67))
            ActualImage = 1;
        else if (isBetween(deg, 67, 112))
            ActualImage = 2;
        else if (isBetween(deg, 122, 157))
            ActualImage = 3;
        else if (isBetween(deg, 157, 202))
            ActualImage = 4;
        else if (isBetween(deg, 202, 247))
            ActualImage = 5;
        else if (isBetween(deg, 247, 292))
            ActualImage = 6;
        else if (isBetween(deg, 292, 337))
            ActualImage = 7;
        else
            ActualImage = 0;

        Image = Pistol[ActualImage];
    }
}