using Tao.Sdl;
/**
 * This class will be the parent of every screen in the video game
 */

class Screen
{
    protected Hardware hardware;
    protected GameController languageController;

    public Screen(Hardware hardware, GameController languageController)
    {
        this.hardware = hardware;
        this.languageController = languageController;
    }

    public virtual void Show()
    {
    }
}
