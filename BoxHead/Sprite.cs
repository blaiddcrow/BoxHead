using System.Collections.Generic;

/*
    * This class is the base class for every sprite of the sprite sheet, 
    * either the main character, enemies, obstacles...
*/
class Sprite
{
   /* 
    * public static Image SpriteSheet = 
    *     new Image("imgs/gauntlet_spritesheet.png", 2385, 768);
   */

    public short X { get; set; }
    public short Y { get; set; }
    public short SpriteX { get; set; }
    public short SpriteY { get; set; }

    public short Width { get; set; }
    public short Height { get; set; }

    public void MoveTo(short x, short y)
    {
        X = x;
        Y = y;
    }

    public bool CollidesWith(Sprite sp) // TODO: Check if it works properly.
    {
        return (X + this.Width > sp.X 
            && X < sp.X + this.Width
            && Y + this.Height > sp.Y 
            && Y < sp.Y + this.Height);
    }

    public bool CollidesWith(List<Sprite> sprites)
    {
        foreach (Sprite sp in sprites)
            if (this.CollidesWith(sp))
                return true;
        return false;
    }
}
