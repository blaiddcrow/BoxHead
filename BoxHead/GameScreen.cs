/*
 * V.0.0.6 21/05/2018 - Javier Saorín Vidal: Modified the management of the screens
 * now the menu screen starts the game and the map editor.
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal: Created all the sceens and a basic
 * structure to manage them.
 */

using System;
using System.Diagnostics;
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
    private Stopwatch stopwatch;

    public static int Points;

    public GameScreen(Hardware hardware, GameController languageController)
        : base(hardware, languageController)
    {
        font = new Font("fonts/PermanentMarker-Regular.ttf", 20);
  
        level = new Level();
        level.ActualLevel = "levels/level1.txt";
        level.Load();
        Character = new Character(300);
        Round = 1;
        enemyGenerator = new EnemyGenerator(hardware);
        enemy = new Enemy(100, 25, 1); // Test enemy.
        Points = Character.Points;
        stopwatch = new Stopwatch();
        roundTextEnglish = lifeTextEnglish = timePlayedTextEnglish =
            ammoTextEnglish = grenadesTextEnglish = new IntPtr();
        roundTextSpanish = lifeTextSpanish = timePlayedTextSpanish =
            ammoTextSpanish = grenadesTextSpanish = new IntPtr();
        initialiceTexts();
    }

    public GameScreen(Hardware hardware, Level level, GameController languageController)
        : this(hardware, languageController)
    {
        this.level = level;
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
        TimeSpan ts = stopwatch.Elapsed;
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
            level.MoveObstacles(moveUp, Character.Speed);
            level.YMap--;
        }
        if (right)
        {
            level.XMap++;
            level.MoveObstacles(moveRight, Character.Speed);
        }
        if (down)
        {
            level.YMap++;
            level.MoveObstacles(moveDown, Character.Speed);
        }
        if (left)
        {
            level.XMap--;
            level.MoveObstacles(moveLeft, Character.Speed);
        }
    }

    public override void Show()
    {
        stopwatch.Start();

        short centeredCharacterX =
            (short)((GameController.SCREEN_WIDTH / 2) - (Character.Width / 2));
        short centeredCharacterY =
           (short)((GameController.SCREEN_HEIGHT / 2) - (Character.Height / 2));

        Character.Image.MoveTo(centeredCharacterX, centeredCharacterY);
        Character.MoveTo(centeredCharacterX, centeredCharacterY);

        enemyGenerator.StartRound(1);

        enemy.MoveTo(0, GameController.SCREEN_HEIGHT);

        short oldX, oldY, oldXMap, oldYMap;

        do
        {
            // 1. Draw everything.
            hardware.ClearScreen();

            hardware.DrawSprite(level.Floor, 0, 0, level.XMap,
                level.YMap, GameController.SCREEN_WIDTH,
                GameController.SCREEN_HEIGHT);

            level.DrawObstacles(hardware);
            hardware.DrawImage(Character.Image);
            hardware.DrawImage(enemy.EnemyImage);

            foreach (Enemy enemy in enemyGenerator.enemies)
            {
                hardware.DrawImage(enemy.EnemyImage);
            }
            drawHud();
            hardware.UpdateScreen();
            // 2. Move character from keyboard input.
            oldX = Character.X;
            oldY = Character.Y;
            oldXMap = level.XMap;
            oldYMap = level.YMap;
            hardware.GetEvents(
                out mouseX, out mouseY, out mouseClickX, out mouseClickY);
            moveCharacter();
            Character.Animate(mouseX, mouseY);
            enemy.PonitToCharacter(Character.X, Character.Y);

            // 3. Move enemies and objects.
            foreach (Enemy enemy in enemyGenerator.enemies)
                enemy.GoToPlayer(Character);

            enemy.GoToPlayer(Character);

            // 4. Check collisions and update game state.
            if (enemy.CharacterIsOnRange(Character))
                enemy.Attack(Character);
        }
        while (Character.Life > 0 && !hardware.IsKeyPressed(Hardware.KEY_ESC));
        stopwatch.Reset();
    }

    public Character GetCharacter()
    {
        return Character;
    }
}
