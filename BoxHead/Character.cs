
using System.Collections.Generic;

class Character : MovableSprite
{
    public double Life { get; set; }
    public bool IsAlive { get; set; }
    public List<Weapon> weapons { get; }
    public int Points { get; set; }
    public Image character;
    public short XMap { get; set; }
    public short YMap { get; set; }

    public Character(double life)
    {
        Life = life;
        Speed = 1;
        IsAlive = true;
        weapons = new List<Weapon>();
        character = new Image("imgs/character/pistol/pistol.png", 100, 100);
        Points = 0;
    }
}