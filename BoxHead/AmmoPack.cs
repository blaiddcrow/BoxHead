
class AmmoPack : Item
{
    public int AmountOfBullets { get; set; }

    public AmmoPack(short x, short y) :
        base(new Image("img/items/ammopack.png", 60, 40), x, y)
    {
        AmountOfBullets = rdn.Next(25, 150);
    }
}
