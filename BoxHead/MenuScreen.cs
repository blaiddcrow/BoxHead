/*
 * V.0.0.6 21/05/2018 - Javier Saorín Vidal: Added methods to manage the options. 
 */
using System;
using System.Collections.Generic;
using Tao.Sdl;

class MenuScreen : Screen
{
    private Image background;
    private Image controls;
    protected Font font;
    private List<IntPtr> optionsEnglishRed;
    private List<IntPtr> optionsEnglishWhite;
    private List<IntPtr> optionsSpanishRed;
    private List<IntPtr> optionsSpanishWhite;
    private IntPtr pressEnterSpanish;
    private IntPtr pressEnterEnglish;
    public int ActualOption { get; set; }
    private bool exit;
    public int StartRound { get; set; }
    private int amountOfOptions { get; set; }

    public MenuScreen(Hardware hardware, GameController languageController)
        : base(hardware, languageController)
    {
        background = new Image("imgs/others/menuBackground.png", 1280, 720);
        controls = new Image("imgs/others/controls.png", 500, 340);
        font = new Font("fonts/PermanentMarker-Regular.ttf", 60);
        optionsEnglishRed = new List<IntPtr>();
        optionsEnglishWhite = new List<IntPtr>();
        optionsSpanishRed = new List<IntPtr>();
        optionsSpanishWhite = new List<IntPtr>();
        pressEnterSpanish = new IntPtr();
        pressEnterEnglish = new IntPtr();
        initialiceOptions();
        exit = false;
        StartRound = 0;
    }

    public override void Show()
    {
        bool isOptionSelected = false;
        background.MoveTo(0, 0);
        controls.MoveTo(
            (short)(GameController.SCREEN_WIDTH - controls.ImageWidth - 10),
            (short)((GameController.SCREEN_HEIGHT / 2) - (controls.ImageHeight / 2)));
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);
            hardware.DrawImage(controls);

            for (int option = 0; option < amountOfOptions; option++)
            {
                if (languageController.isInSpanish)
                {
                    if (option == ActualOption)
                        hardware.WriteText(
                            optionsSpanishWhite[option], 40,
                            (short)(100 * option));
                    else
                        hardware.WriteText(
                            optionsSpanishRed[option], 40,
                            (short)(100 * option));

                    hardware.WriteText(
                        pressEnterSpanish, (short)(GameController.SCREEN_WIDTH - 600),
                        (short)(GameController.SCREEN_HEIGHT - 100));
                }
                else
                {
                    if (option == ActualOption)
                        hardware.WriteText(
                            optionsEnglishWhite[option], 40,
                            (short)(100 * option));
                    else
                        hardware.WriteText(
                            optionsEnglishRed[option], 40,
                            (short)(100 * option));
                    hardware.WriteText(
                        pressEnterSpanish, (short)(GameController.SCREEN_WIDTH - 600),
                        (short)(GameController.SCREEN_HEIGHT - 100));
                }

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
        string[] optionsSpanish =
        {
            "Empezar nueva partida", "Continuar partida",
            "Seleccionar mapa", "Ver puntuaciones", "Editor de mapas",
            "Cambiar idioma", "Salir"
        };
        renderOptionsSpanish(optionsSpanish);

        pressEnterSpanish = SdlTtf.TTF_RenderText_Solid(
            new Font("fonts/PermanentMarker-Regular.ttf", 28).GetFontType(),
            "PULSA ENTER PARA ELEGIR UNA OPCIÓN",
            hardware.White);

        string[] optionsEnglish =
        {
            "Start a new game", "Continue game", "Select map",
            "See highscores", "Map editor", "Switch language",
            "Quit the game"
        };
        renderOptionsEnglish(optionsEnglish);

        pressEnterEnglish = SdlTtf.TTF_RenderText_Solid(
            new Font("fonts/PermanentMarker-Regular.ttf", 30).GetFontType(),
            "PRESS ENTER TO SELECT AN OPTION",
            hardware.White);

        amountOfOptions = optionsEnglish.Length;
    }

    private void renderOptionsSpanish(string[] options)
    {
        foreach (string option in options)
            optionsSpanishRed.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.Red));

        foreach (string option in options)
            optionsSpanishWhite.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.White));
    }

    private void renderOptionsEnglish(string[] options)
    {
        foreach (string option in options)
            optionsEnglishRed.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.Red));

        foreach (string option in options)
            optionsEnglishWhite.Add(SdlTtf.TTF_RenderText_Solid(
                font.GetFontType(), option, hardware.White));
    }

    public void ManageMenu()
    {
        GameScreen game;
        HighscoreScreen highScores;
        MapCreatorScreen mapEditor;
        MapSelectionScreen mapSelector;

        switch (ActualOption)
        {
            case 0: // Start a new game.
                game = new GameScreen(hardware, languageController);
                game.Show();
                break;
            case 1: // Continue game.
                //TODO: Read the start round from a file.
                game = new GameScreen(hardware, StartRound, languageController);
                game.Show();
                break;
            case 2: // Select start round.
                // TODO: Ask the user for the start round.
                mapSelector = new MapSelectionScreen(hardware, languageController);
                mapSelector.Show();
                game = new GameScreen(hardware, mapSelector.GetLevel(), languageController);
                game.Show();
                break;
            case 3: // See highscores.
                 highScores = new HighscoreScreen(hardware, languageController);
                 highScores.Show();
                break;
            case 4: // Map editor.
                mapEditor = new MapCreatorScreen(hardware, languageController);
                mapEditor.Show();
                break;
            case 5: // Switch language
                languageController.SwitchLanguage();
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

        if (key == Hardware.KEY_UP || key == Hardware.KEY_W)
            up = true;
        else if (key == Hardware.KEY_DOWN || key == Hardware.KEY_S)
            down = true;

        if (up)
            if (ActualOption == 0)
                ActualOption = optionsSpanishRed.Count - 1;
            else
                ActualOption--;
        if (down)
            if (ActualOption == optionsSpanishRed.Count - 1)
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
