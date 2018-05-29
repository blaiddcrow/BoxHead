using System;

class Item : StaticSprite
{
    protected Random rdn;

    public Item(Image image, short x, short y)
    {
        rdn = new Random();
        Image = image;
        X = x;
        Y = y;
    }
}
