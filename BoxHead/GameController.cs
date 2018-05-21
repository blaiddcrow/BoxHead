/*
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal: Created the GameController screen
 * which controlls the flow of the program.
 */

class GameController
{
    public const short SCREEN_WIDTH = 1280;
    public const short SCREEN_HEIGHT = 720;

    public void Start()
    {
        Hardware hardware = 
            new Hardware(SCREEN_WIDTH, SCREEN_HEIGHT, 24, false);

        WelcomeScreen welcome = new WelcomeScreen(hardware);
        MenuScreen menu = new MenuScreen(hardware);
        CreditsScreen credits = new CreditsScreen(hardware);

        do
        {
            hardware.ClearScreen();
            welcome.Show();
            menu.Show();
            hardware.ClearScreen();
            credits.Show();
        }
        while (!welcome.GetExit() && !menu.GetExit());
    }
}