using System.Collections.Generic;

class MenuScreen : Screen
{
    public Image background { get; set; }
    protected Font font;
    private List<string> options;
    public int ActualOption { get; set; }
    private bool exit;
    public int StartRound { get; set; }

    public MenuScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("imgs/others/menuBackground.png", 1280, 720);
        font = new Font("fonts/PermanentMarker-Regular.ttf", 60);
        options = new List<string>();
        initialiceOptions();
        exit = false;
        StartRound = 0;
    }

    public override void Show()
    {
        bool insert = false;
        background.MoveTo(0, 0);

        do
        {
            insert = 
                hardware.IsKeyPressed(Hardware.KEY_INSERT); // Not work yet.

            hardware.ClearScreen();
            hardware.DrawImage(background);

            for (int o = 0; o < options.Count; o++)
            {
                if (o == ActualOption)
                    hardware.WriteText(
                        options[o], 40, (short)(100 * o), 255, 255, 255, font);
                else
                    hardware.WriteText(
                        options[o], 40, (short)(100 * o), 255, 0, 0, font);
            }

            hardware.UpdateScreen();

            if (insert)
                ManageMenu();

            MoveBetweenOptions();
        }
        while (!insert);
    }

    private void initialiceOptions()
    {
        options.Add("Start a new game.");
        options.Add("Continue game.");
        options.Add("Select start round.");
        options.Add("See highscores.");
        options.Add("Map editor.");
        options.Add("Options.");
        options.Add("Quit the game.");
    }

    public void ManageMenu()
    {
        GameScreen game;
        HighscoreScreen highScores = new HighscoreScreen(hardware);

        switch (ActualOption)
        {
            case 0: // Start a new game.
                game = new GameScreen(hardware);
                game.Show();
                break;
            case 1: // Continue game.
                //TODO: Read the start round from a file.
                game = new GameScreen(hardware, StartRound);
                game.Show();
                break;
            case 2: // Select start round.
                // TODO: Ask the user for the start round.
                game = new GameScreen(hardware, StartRound);
                game.Show();
                break;
            case 3: // See highscores.
                highScores.Show();
                break;
            case 4: // Map editor.
                // TODO: Create the map editor screen and use it here.
                break;
            case 5: // Options
                // TODO: Create the options screen and use it here.
                break;
            case 6: // Quit the game.
                exit = true;
                break;
            default:
                break;
        }
    }

    private void MoveBetweenOptions()
    {
        bool up = false, down = false;
        int key = hardware.KeyPressed();

        if (key == Hardware.KEY_UP)
            up = true;
        else if (key == Hardware.KEY_DOWN)
            down = true;

        if (up)
        {
            if (ActualOption == 0)
                ActualOption = options.Count - 1;
            else
                ActualOption--;
        }

        if (down)
        {
            if (ActualOption == options.Count - 1)
                ActualOption = 0;
            else
                ActualOption++;
        }
    }

    public bool GetExit()
    {
        return exit;
    }
}
