using Tao.Sdl;
using System;
class WelcomeScreen : Screen
{
    public Image background { get; set; }
    public Image imgTitle { get; set; }
    public Font fontTitle { get; set; }

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        
        background = new Image("imgs/others/wsBackground.png", 1280, 720);
        imgTitle = new Image("imgs/others/boxhead.png",600,300);     
        fontTitle = new Font("fonts/PermanentMarker-Regular.ttf", 20);
    }

    public override void Show()
    {
        short contText=0;

        do
        {
            hardware.ClearScreen();
            hardware.DrawSprite(background, 0, 0, 0, 0
                , GameController.SCREEN_WIDTH,
                GameController.SCREEN_HEIGHT);
            hardware.DrawSprite(imgTitle, 280, 
                (short)(GameController.SCREEN_HEIGHT-contText), 0, 0
                , GameController.SCREEN_WIDTH,
                GameController.SCREEN_HEIGHT);
            hardware.WriteText("Press SPACE to continue",
                (GameController.SCREEN_WIDTH/2)-150,contText,170,0,0,fontTitle);
            hardware.UpdateScreen();

            if (contText<(GameController.SCREEN_HEIGHT/2)+100)
                contText++;
            
        }
        while (!CanGoToNextScreen());  
    }
    
    public bool GetExit()
    {
        return hardware.IsKeyPressed(Hardware.KEY_ESC);
    }

    private bool CanGoToNextScreen()
    {
        return hardware.IsKeyPressed(Hardware.KEY_SPACE);
    }
}
