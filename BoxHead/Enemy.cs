using System;

class Enemy : MovableSprite
{
    public double Life { get; set; }
    public double Damage { get; set; }
    public bool IsAlive { get; set; }
    public Image[] EnemyImages;
    public int ActualImage { get; set; }
    public Image EnemyImage;
    private int movementDirection;
    private int oldMovementDirection;

    public Enemy(double life, double damage, short speed)
    {
        Life = life;
        Damage = damage;
        IsAlive = true;
        Speed = speed;
        EnemyImages = new Image[36];
        initialiceImages();
        EnemyImage = EnemyImages[0];
        oldMovementDirection = movementDirection = 0;
    }

    public void GoToPlayer(Character character, Level level)
    {
        movementDirection = 0;

        short xDiff = (short)(character.X - this.X);
        short YDiff = (short)(character.Y - this.Y);

        short oldX = X;
        short oldY = Y;

        if (xDiff < 0 && YDiff < 0)
        {
            movementDirection = 4;
            X -= Speed;
            Y -= Speed;
        }
        else if (xDiff < 0 && YDiff == 0)
        {
            movementDirection = 3;
            X -= Speed;
        }
        else if (xDiff < 0 && YDiff > 0)
        {
            movementDirection = 5;
            X -= Speed;
            Y += Speed;
        }
        else if (xDiff > 0 && YDiff < 0)
        {
            movementDirection = 6;
            X += Speed;
            Y -= Speed;
        }
        else if (xDiff > 0 && YDiff == 0)
        {
            movementDirection = 1;
            X += Speed;
        }
        else if (xDiff > 0 && YDiff > 0)
        {
            movementDirection = 7;
            X += Speed;
            Y += Speed;
        }
        else if (xDiff == 0 && YDiff < 0)
        {
            movementDirection = 0;
            Y -= Speed;
        }
        else
        {
            movementDirection = 2;
            Y += Speed;
        }

        if (!CollidesWith(character) ||
            (CheckIfCollides(level) && !isMovingInTheSameDirecction()))
            MoveTo(X, Y);
        else
            MoveTo(oldX, oldY);

        oldMovementDirection = movementDirection;

        EnemyImages[ActualImage].MoveTo(X, Y);
    }

    private bool isMovingInTheSameDirecction()
    {
        return movementDirection == oldMovementDirection;
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

        /*
        foreach (Image image in EnemyImages)
        {
            image.X = X;
            image.Y = Y;
        }
        */
    }

    public void MoveEnemy(
        int direction, short speed, Character character)
    {
        switch (direction)
        {
            case 0: // Up to down
                MoveTo(X, (short)(Y + speed));
                EnemyImage.MoveTo(X, (short)(Y + speed));
                break;
            case 1: // Right to left
                MoveTo((short)(X - speed), Y);
                EnemyImage.MoveTo((short)(X - speed), Y);
                break;
            case 2: // Down to up
                MoveTo(X, (short)(Y - speed));
                EnemyImage.MoveTo(X, (short)(Y - speed));
                break;
            case 3: // Left to right
                MoveTo((short)(X + speed), Y);
                EnemyImage.MoveTo((short)(X + speed), Y);
                break;
            default:
                break;
        }
    }

    public bool CharacterIsOnRange(Character character)
    {
        return CollidesWith(character);
    }

    public void Attack(Character character, ref DateTime timeStamp)
    {
        if ((DateTime.Now - timeStamp).TotalMilliseconds > 1000)
        {
            character.Life -= Damage;
            timeStamp = DateTime.Now;
        }
    }

    public bool CollidesWith(Character character)
    {
        return (X + EnemyImage.ImageWidth > character.X
            && X < character.X + EnemyImage.ImageWidth
            && Y + EnemyImage.ImageHeight > character.Y
            && Y < character.Y + EnemyImage.ImageHeight);
    }

    public bool CollidesWith(Obstacle o)
    {
        return (X + EnemyImage.ImageWidth > o.X
            && X < o.X + EnemyImage.ImageWidth
            && Y + EnemyImage.ImageHeight > o.Y
            && Y < o.Y + EnemyImage.ImageHeight);
    }

    public bool CheckIfCollides(Level level)
    {
        bool hasCollided = false;
        for (int i = 0; i < level.Obstacles.Count && !hasCollided; i++)
            hasCollided = CollidesWith(level.Obstacles[i]);
        return hasCollided;
    }
}


