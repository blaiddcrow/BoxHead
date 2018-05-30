using System;
using System.Collections.Generic;
using System.IO;

class MapSelectionScreen : Screen
{
    private Image background;
    private List<IntPtr> levelsTexts;
    private List<string> levelPaths;
    private string selectedLevel;

    public MapSelectionScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("imgs/others/menuBackground.png", 1280, 720);
        levelsTexts = new List<IntPtr>();
        levelPaths = new List<string>();
        initialiceLevels();
        selectedLevel = "levels/level1.txt";
    }

    private void initialiceLevels()
    {
        if (Directory.Exists("./levels"))
        {
            string[] fileList = Directory.GetFiles("./levels");
            foreach (string file in fileList)
            {
                if (file.StartsWith("level"))
                {
                    levelPaths.Add("levels/" + file);
                }
            }
        }
    }

    public override void Show()
    {
        base.Show();
    }
}
