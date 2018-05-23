
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
        Pistol[0] = new Image("imgs/character/pistol/pistol0.png", 100, 100);
        Pistol[1] = new Image("imgs/character/pistol/pistol45.png", 141, 141);
        Pistol[2] = new Image("imgs/character/pistol/pistol90.png", 100, 100);
        Pistol[3] = new Image("imgs/character/pistol/pistol135.png", 141, 141);
        Pistol[4] = new Image("imgs/character/pistol/pistol180.png", 100, 100);
        Pistol[5] = new Image("imgs/character/pistol/pistol225.png", 141, 141);
        Pistol[6] = new Image("imgs/character/pistol/pistol270.png", 100, 100);
        Pistol[7] = new Image("imgs/character/pistol/pistol315.png", 141, 141);

        foreach (Image image in Pistol)
        {
            image.X = 
                (short)((GameController.SCREEN_WIDTH / 2) - (Width / 2));
            image.Y = 
                (short)((GameController.SCREEN_HEIGHT / 2) - (Height / 2));
        }

    }

    public void Animate(int mouseX, int mouseY)
    {
        short width = GameController.SCREEN_WIDTH;
        short height = GameController.SCREEN_HEIGHT;

        short widthQuarter = (short)(width / 4);
        short heightQuarter = (short)(height / 4);

        short widthHalf = (short)(width / 2);
        short heightHalf = (short)(height / 2);

        if (mouseY < heightQuarter)
        {
            if (mouseX < widthQuarter)
                ActualImage = 7;
            else if (mouseX > widthQuarter && mouseX < width - widthQuarter)
                ActualImage = 0;
            else if (mouseX > width - widthQuarter)
                ActualImage = 1;
        }
        else if ((mouseY > heightQuarter && mouseY < heightHalf) ||
                (mouseY > heightHalf && mouseY < height - heightQuarter))
        {
            if (mouseX < widthHalf)
                ActualImage = 6;
            else if (mouseX > widthHalf)
                ActualImage = 2;
        }
        else if (mouseY > height - heightQuarter)
        {
            if (mouseX < widthQuarter)
                ActualImage = 5;
            else if (mouseX > widthQuarter && mouseX < width - widthQuarter)
                ActualImage = 4;
            else if (mouseX > width - widthQuarter)
                ActualImage = 3;
        }

        Image = Pistol[ActualImage];
    }
}