using System;
using System.Collections.Generic;
using System.IO;
using Tao.Sdl;

class MapSelectionScreen : Screen
{
    private Image background;
    private List<IntPtr> levelsTextRed;
    private List<IntPtr> levelsTextWhite;
    private IntPtr pressEnterEnglish;
    private IntPtr pressEnterSpanish;
    private List<string> levelPaths;
    private string selectedLevel;
    private Font font;
    private int amountOfTexts;

    public int ActualLevel { get; set; }

    public MapSelectionScreen(Hardware hardware, GameController languageController)
        : base(hardware, languageController)
    {
        font = new Font("fonts/PermanentMarker-Regular.ttf", 20);
        background = new Image("imgs/others/menuBackground.png", 1280, 720);
        levelsTextRed = new List<IntPtr>();
        levelsTextWhite = new List<IntPtr>();
        levelPaths = new List<string>();
        pressEnterEnglish = new IntPtr();
        pressEnterSpanish= new IntPtr();
        initialiceLevelTexts();
        amountOfTexts = levelsTextRed.Count;
        ActualLevel = 0;
        selectedLevel = "levels/level1.txt";
    }

    private void initialiceLevelTexts()
    {
        if (Directory.Exists("./levels"))
        {
            string[] fileList = Directory.GetFiles("./levels");
            foreach (string file in fileList)
            {
                if (file.StartsWith("./levels\\level"))
                {
                    levelPaths.Add(file);

                    int amount = file.LastIndexOf('.') - 1 - file.LastIndexOf('\\');

                    levelsTextRed.Add(SdlTtf.TTF_RenderText_Solid(
                    font.GetFontType(), file.Substring(
                        file.LastIndexOf('\\')+1, amount), hardware.Red));

                    levelsTextWhite.Add(SdlTtf.TTF_RenderText_Solid(
                    font.GetFontType(), file.Substring(
                        file.LastIndexOf('\\')+1, amount), hardware.White));
                }
            }
        }

        pressEnterEnglish = SdlTtf.TTF_RenderText_Solid(
        new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
        "PRESS ENTER TO SELECT A LEVEL", hardware.White);

        pressEnterEnglish = SdlTtf.TTF_RenderText_Solid(
        new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
        "PULSA ENTER PARA SELECCIONAR UN NIVEL", hardware.White);
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
            if (ActualLevel == 0)
                ActualLevel = levelsTextRed.Count - 1;
            else
                ActualLevel--;
        if (down)
            if (ActualLevel == levelsTextRed.Count - 1)
                ActualLevel = 0;
            else
                ActualLevel++;

        return optionSelected;
    }

    public Level GetLevel()
    {
        Level newLevel = new Level();
        newLevel.ActualLevel = selectedLevel;
        return newLevel;
    }

    public override void Show()
    {
        bool isOptionSelected = false;
        background.MoveTo(0, 0);

        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);

            for (int level = 0; level < amountOfTexts; level++)
            {
                if (level == ActualLevel)
                    hardware.WriteText(
                        levelsTextWhite[level], 40, (short)(50 * level));
                else
                    hardware.WriteText(
                        levelsTextRed[level], 40, (short)(50 * level));
            }

            if (languageController.isInSpanish)
            {
                hardware.WriteText(
                    pressEnterSpanish, (short)(GameController.SCREEN_WIDTH / 2 - 150),
                    (short)(GameController.SCREEN_HEIGHT - 30));
            }
            else
            {
                hardware.WriteText(
                    pressEnterEnglish, (short)(GameController.SCREEN_WIDTH / 2 - 150),
                    (short)(GameController.SCREEN_HEIGHT - 30));
            }

            hardware.UpdateScreen();
            isOptionSelected = CheckInput();

            if (isOptionSelected)
                selectedLevel = levelPaths[ActualLevel];       
        }
        while (!isOptionSelected);
        GameScreen game = new GameScreen(
            hardware, new Level(selectedLevel), languageController);
        game.Show();
    }
}
