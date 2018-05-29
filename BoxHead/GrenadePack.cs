
class GrenadePack : Item
{
    public int AmountOfGrenades { get; set; }

    public GrenadePack(short x, short y) :
        base(new Image("img/items/grenadepack.png", 60, 40), x, y)
    {
        AmountOfGrenades = rdn.Next(1, 10);
    }
}
