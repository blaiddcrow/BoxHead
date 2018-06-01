/*
 * V.0.0.6 21/05/2018 - Javier Saorín Vidal: Modified the management of the screens
 * now the menu screen starts the game and the map editor.
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal: Created all the sceens and a basic
 * structure to manage them.
 */

using System;
using System.Diagnostics;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    protected EnemyGenerator enemyGenerator;
    protected ItemGenerator itemGenerator;
    private IntPtr roundTextEnglish;
    private IntPtr lifeTextEnglish;
    private IntPtr timePlayedTextEnglish;
    private IntPtr ammoTextEnglish;
    private IntPtr grenadesTextEnglish;
    private IntPtr roundTextSpanish;
    private IntPtr lifeTextSpanish;
    private IntPtr timePlayedTextSpanish;
    private IntPtr ammoTextSpanish;
    private IntPtr grenadesTextSpanish;
    protected Enemy enemy; // Test enemy.
    protected Level level;
    public Character Character { get; set; }
    protected Font font;
    private int mouseX, mouseY;
    private int mouseClickX, mouseClickY;
    public int Round { get; set; }
    private Stopwatch timePlayedTimer;
    private byte oldCharacterMovementDirection;

    public static int Points;

    public GameScreen(Hardware hardware, GameController languageController)
        : base(hardware, languageController)
    {
        font = new Font("fonts/PermanentMarker-Regular.ttf", 20);
        enemyGenerator = new EnemyGenerator(hardware);
        level = new Level();
        level.Load();
        Character = new Character(300);
        Round = 1;
        itemGenerator = new ItemGenerator();
        enemy = new Enemy(100, 15, 1); // Test enemy.
        Points = Character.Points;
        timePlayedTimer = new Stopwatch();
        roundTextEnglish = lifeTextEnglish = timePlayedTextEnglish =
            ammoTextEnglish = grenadesTextEnglish = new IntPtr();
        roundTextSpanish = lifeTextSpanish = timePlayedTextSpanish =
            ammoTextSpanish = grenadesTextSpanish = new IntPtr();
        initialiceTexts();
        oldCharacterMovementDirection = Character.MovementDirection;
    }

    public GameScreen(Hardware hardware, Level level, GameController languageController)
        : this(hardware, languageController)
    {
        this.level = level;
        level.Load();
    }

    public GameScreen(Hardware hardware, int round, GameController languageController)
        : this(hardware, languageController)
    {
        Round = round;
    }

    private void initialiceTexts()
    {
        roundTextEnglish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "ROUND: ", hardware.Red);
        lifeTextEnglish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "LIFE: ", hardware.Red);
        ammoTextEnglish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "AMMO: ", hardware.Red);
        grenadesTextEnglish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "GRENADES: ", hardware.Red);
        timePlayedTextEnglish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "TIME PLAYED: ", hardware.Red);

        roundTextSpanish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "RONDA: ", hardware.Red);
        lifeTextSpanish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "VIDA: ", hardware.Red);
        ammoTextSpanish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "MUNICIÓN: ", hardware.Red);
        grenadesTextSpanish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "GRENADAS: ", hardware.Red);
        timePlayedTextSpanish = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "TIEMPO JUGADO: ", hardware.Red);

        hardware.InitialiceTexts();
    }

    private string getPlatedTime()
    {
        TimeSpan ts = timePlayedTimer.Elapsed;
        return String.Format(
            "{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void drawHud()
    {
        if (languageController.isInSpanish)
        {
            hardware.WriteText(
                roundTextSpanish, GameController.SCREEN_WIDTH / 2 - 50, 10);
            hardware.WriteText(
                Round.ToString(), GameController.SCREEN_WIDTH / 2 + 50, 10);

            hardware.WriteText(
                lifeTextSpanish, 10, GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText(
                Character.Life.ToString(), 70, GameController.SCREEN_HEIGHT - 30);

            hardware.WriteText(timePlayedTextSpanish, GameController.SCREEN_WIDTH / 2 - 90,
                GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText(getPlatedTime(), GameController.SCREEN_WIDTH / 2 + 100,
                GameController.SCREEN_HEIGHT - 30);

            hardware.WriteText(ammoTextSpanish, GameController.SCREEN_WIDTH - 200,
               GameController.SCREEN_HEIGHT - 60);
            hardware.WriteText("" + Character.weapons[Character.ActualWeapon].Ammo,
                GameController.SCREEN_WIDTH - 50, GameController.SCREEN_HEIGHT - 60);

            hardware.WriteText(grenadesTextSpanish, GameController.SCREEN_WIDTH - 200,
               GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText("" + Character.AmountOfgrenades,
                GameController.SCREEN_WIDTH - 25, GameController.SCREEN_HEIGHT - 30);
        }
        else
        {
            hardware.WriteText(
                roundTextEnglish, GameController.SCREEN_WIDTH / 2 - 50, 10);
            hardware.WriteText(
                Round.ToString(), GameController.SCREEN_WIDTH / 2 + 50, 10);

            hardware.WriteText(
                lifeTextEnglish, 10, GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText(
                Character.Life.ToString(), 70, GameController.SCREEN_HEIGHT - 30);

            hardware.WriteText(timePlayedTextEnglish, GameController.SCREEN_WIDTH / 2 - 70,
                GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText(getPlatedTime(), GameController.SCREEN_WIDTH / 2 + 100,
                GameController.SCREEN_HEIGHT - 30);

            hardware.WriteText(ammoTextEnglish, GameController.SCREEN_WIDTH - 150,
               GameController.SCREEN_HEIGHT - 60);
            hardware.WriteText("" + Character.weapons[Character.ActualWeapon].Ammo,
                GameController.SCREEN_WIDTH - 50, GameController.SCREEN_HEIGHT - 60);

            hardware.WriteText(grenadesTextEnglish, GameController.SCREEN_WIDTH - 150,
               GameController.SCREEN_HEIGHT - 30);
            hardware.WriteText("" + Character.AmountOfgrenades,
                GameController.SCREEN_WIDTH - 25, GameController.SCREEN_HEIGHT - 30);
        }
    }

    private void moveBullets()
    {
        foreach (Bullet b in Character.Glock.Bullets)
        {
            short xDiff = (short)(b.XDirection- b.X);
            short YDiff = (short)(b.YDirection- b.Y);

            if (xDiff < 0 && YDiff < 0)
            {
                b.X -= b.Speed;
                b.Y -= b.Speed;
            }
            else if (xDiff < 0 && YDiff == 0)
            {
                b.X -= b.Speed;
            }
            else if (xDiff < 0 && YDiff > 0)
            {
                b.X -= b.Speed;
                b.Y += b.Speed;
            }
            else if (xDiff > 0 && YDiff < 0)
            {
                b.X += b.Speed;
                b.Y -= b.Speed;
            }
            else if (xDiff > 0 && YDiff == 0)
            {
                b.X += b.Speed;
            }
            else if (xDiff > 0 && YDiff > 0)
            {
                b.X += b.Speed;
                b.Y += b.Speed;
            }
            else if (xDiff == 0 && YDiff < 0)
            {
                b.Y -= b.Speed;
            }
            else
            {
                b.Y += b.Speed;
            }
        }
    }

    private void moveCharacter()
    {
        short moveUp = 0;
        short moveRight = 1;
        short moveDown = 2;
        short moveLeft = 3;

        bool left = 
            hardware.IsKeyPressed(Hardware.KEY_LEFT) ||
            hardware.IsKeyPressed(Hardware.KEY_A);
        bool right = 
            hardware.IsKeyPressed(Hardware.KEY_RIGHT) ||
            hardware.IsKeyPressed(Hardware.KEY_D);
        bool up = 
            hardware.IsKeyPressed(Hardware.KEY_UP) ||
            hardware.IsKeyPressed(Hardware.KEY_W);
        bool down = 
            hardware.IsKeyPressed(Hardware.KEY_DOWN) ||
            hardware.IsKeyPressed(Hardware.KEY_S);

        if (up)
        {
            Character.MovementDirection = 0;
            if (Character.CheckIfCollides(level) && !isMovingInTheSameDirecction() ||
                !Character.CheckIfCollides(level))
            {
                level.YMap--;
                level.MoveObstacles(moveUp, Character.Speed);
                moveEnemies(moveUp);
                oldCharacterMovementDirection = Character.MovementDirection;
            }
        }
        if (right)
        {
            Character.MovementDirection = 1;
            if (Character.CheckIfCollides(level) && !isMovingInTheSameDirecction() ||
                !Character.CheckIfCollides(level))
            {
                level.XMap++;
                level.MoveObstacles(moveRight, Character.Speed);
                moveEnemies(moveRight);
                oldCharacterMovementDirection = Character.MovementDirection;
            }
            
        }
        if (down)
        {
            Character.MovementDirection = 2;
            if (Character.CheckIfCollides(level) && !isMovingInTheSameDirecction() ||
                !Character.CheckIfCollides(level))
            {
                level.YMap++;
                level.MoveObstacles(moveDown, Character.Speed);
                moveEnemies(moveDown);
                oldCharacterMovementDirection = Character.MovementDirection;
            }
        }
        if (left)
        {
            Character.MovementDirection = 3;
            if (Character.CheckIfCollides(level) && !isMovingInTheSameDirecction() ||
                !Character.CheckIfCollides(level))
            {
                level.XMap--;
                level.MoveObstacles(moveLeft, Character.Speed);
                moveEnemies(moveLeft);
                oldCharacterMovementDirection = Character.MovementDirection;
            }
        }
    }

    private bool isMovingInTheSameDirecction()
    {
        return Character.MovementDirection == oldCharacterMovementDirection;
    }

    private void moveEnemies(int direction)
    {
        foreach (Enemy e in enemyGenerator.enemies)
        {
            e.MoveEnemy(direction, Character.Speed, Character);
        }

        enemy.MoveEnemy(direction, Character.Speed, Character);
    }

    public override void Show()
    {
        timePlayedTimer.Start();
        DateTime timeStampFromLastShot = DateTime.Now;
        DateTime timeStampFromLastDamage = DateTime.Now;

        short centeredCharacterX =
            (short)((GameController.SCREEN_WIDTH / 2) - (Character.Width / 2));
        short centeredCharacterY =
           (short)((GameController.SCREEN_HEIGHT / 2) - (Character.Height / 2));

        Character.Image.MoveTo(centeredCharacterX, centeredCharacterY);
        Character.MoveTo(centeredCharacterX, centeredCharacterY);

        enemyGenerator.InitialiceRound(Round);
        enemyGenerator.SetEnemiesSpawnpoints();

        enemy.MoveTo(0, GameController.SCREEN_HEIGHT);

        short oldX, oldY, oldXMap, oldYMap;

        do
        {
            // 1. Draw everything.
            hardware.ClearScreen();

            hardware.DrawSprite(level.Floor, 0, 0, 0,
                0, GameController.SCREEN_WIDTH,
                GameController.SCREEN_HEIGHT);

            level.DrawObstacles(hardware);
            hardware.DrawImage(Character.Image);
            hardware.DrawImage(enemy.EnemyImage);

            foreach (Enemy enemy in enemyGenerator.enemies)
            {
                hardware.DrawImage(enemy.EnemyImage);
            }

            foreach (Bullet bullet in Character.
                weapons[Character.ActualWeapon].Bullets)
            {
                hardware.DrawImage(bullet.Image);
            }

            drawHud();
            hardware.UpdateScreen();
            // 2. Move character from keyboard input.
            oldX = Character.X;
            oldY = Character.Y;
            oldXMap = level.XMap;
            oldYMap = level.YMap;

            hardware.GetEvents(
                out mouseX, out mouseY,
                out mouseClickX, out mouseClickY);

            if (mouseClickX != hardware.GetOldMouseClickX() &&
                mouseClickX != hardware.GetOldMouseClickY())
            {
                Character.weapons[Character.ActualWeapon].
                    Bullets.Add(new Bullet(
                        (short)mouseClickX, (short)mouseClickY));
            }

            moveBullets();
            moveCharacter();
            Character.Animate(mouseX, mouseY);

            // 3. Move enemies and objects.
            foreach (Enemy enemy in enemyGenerator.enemies)
            {
                enemy.PonitToCharacter(Character.X, Character.Y);
                enemy.GoToPlayer(Character, level);
            }

            enemy.PonitToCharacter(Character.X, Character.Y);
            enemy.GoToPlayer(Character, level);

            // 4. Check collisions and update game state.
            if (enemy.CharacterIsOnRange(Character))
            {
                enemy.Attack(Character, ref timeStampFromLastDamage);
            }
        }
        while (Character.Life > 0 && !hardware.IsKeyPressed(Hardware.KEY_ESC));
        timePlayedTimer.Reset();
    }

    public Character GetCharacter()
    {
        return Character;
    }
}
