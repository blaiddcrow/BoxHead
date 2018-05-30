/*
 * V.0.0.12 29/05/2018 - Javier Saorín Vidal: Added methods to fill a list of 
 * items with random items.
 */

using System;
using System.Collections.Generic;

class ItemGenerator
{
    private Random rdn;

    public List<Item> Items { get; set; }
    private int maxAmountOfHealthPacks;
    private int maxAmountOfAmmoPacks;
    private int maxAmountOfGrenadePacks;
    public ItemGenerator()
    {
        rdn = new Random();
        Items = new List<Item>();
    }

    public void GenerateItems()
    {
        maxAmountOfHealthPacks = rdn.Next(5);
        maxAmountOfAmmoPacks = rdn.Next(15);
        maxAmountOfGrenadePacks = rdn.Next(3);

        int amountOfHealthPacks = 0;
        int amountOfAmmoPacks = 0;
        int amountOfGrenadePacks = 0;

        do
        {
            int luckyNumber = rdn.Next(1, 3);

            switch (luckyNumber)
            {
                case 1:
                    if (amountOfHealthPacks < maxAmountOfHealthPacks)
                    {
                        Items.Add( new HealthPack(
                            (short)(rdn.Next(0, Level.MAP_WIDTH)), 
                            (short)(rdn.Next(0, Level.MAP_HEIGHT))));
                    }
                    break;
                case 2:
                    if (amountOfHealthPacks < maxAmountOfHealthPacks)
                    {
                        Items.Add(new AmmoPack(
                            (short)(rdn.Next(0, Level.MAP_WIDTH)),
                            (short)(rdn.Next(0, Level.MAP_HEIGHT))));
                    }
                    break;
                case 3:
                    if (amountOfHealthPacks < maxAmountOfHealthPacks)
                    {
                        Items.Add(new GrenadePack(
                            (short)(rdn.Next(0, Level.MAP_WIDTH)),
                            (short)(rdn.Next(0, Level.MAP_HEIGHT))));
                    }
                    break;
                default:
                    break;
            }
        }
        while (amountOfHealthPacks < maxAmountOfHealthPacks &&
            amountOfAmmoPacks < maxAmountOfAmmoPacks &&
            amountOfGrenadePacks < maxAmountOfGrenadePacks);
    }
}
