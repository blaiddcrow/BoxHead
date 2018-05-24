using System;

class Enemy : MovableSprite
{
    public double Life { get; set; }
    public double Damage { get; set; }
    public bool IsAlive { get; set; }
    public Image[] EnemyImages;
    public int ActualImage { get; set; }
    public Image EnemyImage;

    public Enemy(double life, double damage, short speed)
    {
        Life = life;
        Damage = damage;
        IsAlive = true;
        Speed = speed;
        EnemyImages = new Image[36];
        initialiceImages();
        EnemyImage = EnemyImages[0];
    }

    public void GoToPlayer(Character character)
    {
        short xDiff = (short)(character.X - this.X);
        short YDiff = (short)(character.Y - this.Y);

        if (xDiff < 0 && YDiff < 0)
        { 
            X -= Speed;
            Y -= Speed;
        }
        else if (xDiff < 0 && YDiff == 0)
        {
            X -= Speed;
        }
        else if (xDiff < 0 && YDiff > 0)
        {
            X -= Speed;
            Y += Speed;
        }
        else if (xDiff > 0 && YDiff < 0)
        {
            X += Speed;
            Y -= Speed;
        }
        else if (xDiff > 0 && YDiff == 0)
        {
            X += Speed;
        }
        else if (xDiff > 0 && YDiff > 0)
        {
            X += Speed;
            Y += Speed;
        }
        else if (xDiff == 0 && YDiff < 0)
        {
            Y -= Speed;
        }
        else
        {
            Y += Speed;
        }

        if (!CollidesWith(character))
            MoveTo(X, Y);

        EnemyImages[ActualImage].MoveTo(X, Y);
    }

    public void PonitToCharacter(int characterX, int characterY)
    {
        double deg =
            Math.Atan2(Y - characterY, X - characterX) * 180 / Math.PI + 180;

        for (int i = 0; i < 36; i++)
            if (isBetween(deg, i * 10, (i + 1) * 10))
                ActualImage = i;

        EnemyImage = EnemyImages[ActualImage];
    }

    private bool isBetween(double number, int min, int max)
    {
        return number > min && number < max;
    }

    private void initialiceImages()
    {
        int imageDegrees = 0;

        for (int i = 0; i < EnemyImages.Length; i++)
        {
            EnemyImages[i] = new Image("imgs/enemy/zombie" +
                imageDegrees + ".png", 60, 60);
            imageDegrees += 10;
        }

        foreach (Image image in EnemyImages)
        {
            image.X = X;
            image.Y = Y;
        }
    }

    public bool CharacterIsOnRange(Character character)
    {
        return CollidesWith(character);
    }

    public void Attack(Character character)
    {
        character.Life -= Damage;
    }
}
