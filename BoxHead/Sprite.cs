﻿using System.Collections.Generic;

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

    public Image Image { get; set; }

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
}
