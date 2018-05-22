/*
 * V.0.0.6 21/05/2018 - Javier Saorín Vidal: Added methods to manage the options. 
 */
using System;
using System.Collections.Generic;
using Tao.Sdl;

class MenuScreen : Screen
{
    public Image background { get; set; }
    protected Font font;
    private List<IntPtr> optionsRed;
    private List<IntPtr> optionsWhite;
    public int ActualOption { get; set; }
    private bool exit;
    public int StartRound { get; set; }
    private int amountOfOptions { get; set; }

    public MenuScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("imgs/others/menuBackground.png", 1280, 720);
        font = new Font("fonts/PermanentMarker-Regular.ttf", 60);
        optionsRed = new List<IntPtr>();
        optionsWhite = new List<IntPtr>();
        initialiceOptions();
        exit = false;
        StartRound = 0;
    }

    public override void Show()
    {
        bool isOptionSelected = false;
        background.MoveTo(0, 0);

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);

            for (int option = 0; option < amountOfOptions; option++)
            {
                if (option == ActualOption)
                    hardware.WriteText(
                        optionsWhite[option], 40, (short)(100 * option));
                else
                    hardware.WriteText(
                        optionsRed[option], 40, (short)(100 * option));
            }
            hardware.UpdateScreen();
            isOptionSelected = CheckInput();

            if (isOptionSelected)
                ManageMenu();
        }
        while (!isOptionSelected);
    }

    private void initialiceOptions()
    {
        string[] options =
        {
            "Start a new game.", "Continue game.", "Select start round.",
            "See highscores.", "Map editor.", "Options.", "Quit the game."
        };

        amountOfOptions = options.Length;

        foreach (string option in options)
            optionsRed.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.Red));

        foreach (string option in options)
            optionsWhite.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.White));
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

    private bool CheckInput()
    {
        int key = hardware.KeyPressed();

        bool up = false, down = false;
        bool optionSelected = (key == Hardware.KEY_ENTER);

        if (key == Hardware.KEY_UP)
            up = true;
        else if (key == Hardware.KEY_DOWN)
            down = true;

        if (up)
            if (ActualOption == 0)
                ActualOption = optionsRed.Count - 1;
            else
                ActualOption--;

        if (down)
            if (ActualOption == optionsRed.Count - 1)
                ActualOption = 0;
            else
                ActualOption++;

        return optionSelected;
    }

    public bool GetExit()
    {
        return exit;
    }
}
