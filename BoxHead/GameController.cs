/*
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal: Created the GameController screen
 * which controlls the flow of the program.
 */

using System.IO;

class GameController
{
    public const short SCREEN_WIDTH = 1280;
    public const short SCREEN_HEIGHT = 720;
    public bool isInSpanish { get; set; }

    public GameController()
    {
        isInSpanish = getLanguage();
    }

    public void Start()
    {
        Hardware hardware = 
            new Hardware(SCREEN_WIDTH, SCREEN_HEIGHT, 24, false);

        WelcomeScreen welcome = new WelcomeScreen(hardware, this);
        MenuScreen menu = new MenuScreen(hardware, this);
        CreditsScreen credits = new CreditsScreen(hardware, this);

        hardware.ClearScreen();
        welcome.Show();

        do
        {
            hardware.ClearScreen();
            menu.Show();
            hardware.ClearScreen();
            credits.Show();
        }
        while (!menu.GetExit());
        SetLanguaje();
    }

    private bool getLanguage()
    {
        if (!File.Exists("language.dat"))
            return false;
        using (StreamReader input = new StreamReader("language.dat"))
        {
            string line = input.ReadLine().ToLower();
            if (line == "spanish")
                return true;
            else
                return false;
        }
    }
    
    public void SwitchLanguage()
    {
        isInSpanish = !isInSpanish;
    }

    public void SetLanguaje()
    {
        using (StreamWriter output = new StreamWriter("language.dat"))
        {
            if (isInSpanish)
                output.WriteLine("spanish");
            else
                output.WriteLine("english");
        }   
    }
}