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
    private IntPtr roundText;
    private IntPtr lifeText;
    private IntPtr timePlayedText;
    private IntPtr ammoText;
    private IntPtr grenadesText;
    protected Enemy enemy; // Test enemy.
    protected Level level;
    public Character Character { get; set; }
    protected Font font;
    private int mouseX, mouseY;
    private int mouseClickX, mouseClickY;
    public int Round { get; set; }
    private Stopwatch stopwatch;

    public static int Points;

    public GameScreen(Hardware hardware) : base(hardware)
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
        roundText = lifeText = timePlayedText =
            ammoText = grenadesText = new IntPtr();
        updateTexts();
    }

    public GameScreen(Hardware hardware, int round) : this(hardware)
    {
        Round = round;
    }

    private void updateTexts()
    {
        roundText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "ROUND: "+ Round, hardware.Red);
        lifeText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "LIFE: " + Character.Life, hardware.Red);
        ammoText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "AMMO: " + Character.weapons[Character.ActualWeapon].Ammo,
                hardware.Red);
        grenadesText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "GRENADES: " + Character.AmountOfgrenades, hardware.Red);  
        timePlayedText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                "TIME PLAYED: " + getPlatedTime() ,hardware.Red);
    }

    private string getPlatedTime()
    {
        TimeSpan ts = stopwatch.Elapsed;
        return String.Format(
            "{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void drawHud()
    {
        hardware.WriteText(
                    roundText, GameController.SCREEN_WIDTH / 2 - 50, 10);
        hardware.WriteText(
                    lifeText, 10, GameController.SCREEN_HEIGHT - 30);
        hardware.WriteText(timePlayedText, GameController.SCREEN_WIDTH / 2 - 70,
            GameController.SCREEN_HEIGHT - 30);
        hardware.WriteText(ammoText, GameController.SCREEN_WIDTH - 150,
           GameController.SCREEN_HEIGHT - 60);
        hardware.WriteText(grenadesText, GameController.SCREEN_WIDTH - 150,
           GameController.SCREEN_HEIGHT - 30);
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

            updateTexts();
        }
        while (Character.Life > 0 && !hardware.IsKeyPressed(Hardware.KEY_ESC));
        stopwatch.Reset();
    }

    public Character GetCharacter()
    {
        return Character;
    }
}
