using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tao.Sdl;

class HighscoreScreen : Screen
{
    private Image background;
    private IntPtr enterName;
    private string actualName;

    Dictionary<string, int> oldPlayers;
    IDictionaryEnumerator enumerator;

    public HighscoreScreen(Hardware hardware) : base(hardware)
    {
        background =
            new Image("imgs/others/menuBackground.png",
            GameController.SCREEN_WIDTH, GameController.SCREEN_HEIGHT);
        enterName = new IntPtr();
        initialiceTexts();
        oldPlayers = new Dictionary<string, int>();
        loadScores();
    }

    public string GetActualName() { return actualName; }

    public void SetActualName(string newName)
    {
        actualName = newName;
    }

    public override void Show()
    {        
        background.MoveTo(0, 0);
        do
        {
            // Draw everything.
            hardware.ClearScreen();
            hardware.DrawImage(background);
            hardware.WriteText(enterName, 10 , 10);
            hardware.UpdateScreen();
        }
        while (!getExit());
    }

    private IntPtr parseIntPtr(string text, Sdl.SDL_Color color)
    {
        return SdlTtf.TTF_RenderText_Solid(
            new Font("fonts/Creepster-Regular.ttf", 30).GetFontType(),
            text, color);
    }

    private void initialiceTexts()
    {
        enterName = SdlTtf.TTF_RenderText_Solid(
            new Font("fonts/Creepster-Regular.ttf", 30).GetFontType(),
            "ENTER PLAYER NAME: ",
            hardware.Red);  

    }

    private void loadScores()
    {
        enumerator = oldPlayers.GetEnumerator();
        try
        {
            if (!File.Exists("playerScores.dat"))
                Environment.Exit(1);

            StreamReader input = new StreamReader("playerScores.dat");
            string line = "";

            do
            {
                string[] parts;
                int points;
                string name;

                line = input.ReadLine();
                if (line != null)
                {
                    parts = line.Split();
                    name = parts[0].ToLower();
                    bool isParsed = int.TryParse(parts[1], out points);

                    if (isParsed)
                        if (oldPlayers.ContainsKey(name))
                            while (enumerator.MoveNext())
                                if ((string)enumerator.Key == name)
                                    oldPlayers[name] = points;
                        else
                            oldPlayers.Add(parts[0].ToLower(), points);
                }
            }
            while (line != null);

            input.Close();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
            Environment.Exit(1);
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long.");
            Environment.Exit(1);
        }
        catch (IOException)
        {
            Console.WriteLine("Input/Output error.");
            Environment.Exit(1);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Environment.Exit(1);
        }
    }

    private string getUsername()
    {
        int key = hardware.KeyPressed();

        switch (key)
        {
            case Hardware.KEY_A:
                break;
            case Hardware.KEY_B:
                break;
            case Hardware.KEY_C:
                break;
            case Hardware.KEY_D:
                break;
            case Hardware.KEY_E:
                break;
            case Hardware.KEY_F:
                break;
            case Hardware.KEY_G:
                break;
            case Hardware.KEY_H:
                break;
            case Hardware.KEY_I:
                break;
            case Hardware.KEY_J:
                break;
            case Hardware.KEY_K:
                break;
            case Hardware.KEY_L:
                break;
            case Hardware.KEY_M:
                break;
            case Hardware.KEY_N:
                break;
            case Hardware.KEY_O:
                break;
            case Hardware.KEY_P:
                break;
            case Hardware.KEY_Q:
                break;
            case Hardware.KEY_R:
                break;
            case Hardware.KEY_S:
                break;
            case Hardware.KEY_T:
                break;
            case Hardware.KEY_U:
                break;
            case Hardware.KEY_V:
                break;
            case Hardware.KEY_W:
                break;
            case Hardware.KEY_X:
                break;
            case Hardware.KEY_Y:
                break;
            case Hardware.KEY_Z:
                break;
            case Hardware.KEY_BACKSPACE:
                break;
            default:
                break;
        }
        return "";
    }

    private bool getExit()
    {
        return hardware.IsKeyPressed(Hardware.KEY_ESC);
    }
}
