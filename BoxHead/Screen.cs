using Tao.Sdl;
/**
 * This class will be the parent of every screen in the video game
 */

class Screen
{
    protected Hardware hardware;

    public Screen(Hardware hardware)
    {
        this.hardware = hardware;
    }

    public virtual void Show()
    {
    }
}
