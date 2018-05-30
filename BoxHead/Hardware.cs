/*
 * V.0.0.8 23/05/2018 - Javier Saorín Vidal: Added GetEvents method to manage
 * the mouse input.
 */

using System;
using Tao.Sdl;

/**
* This class will manage every hardware issue: screen resolution,
* keyboard input and some other aspects.
**/

class Hardware
{
    public const int KEY_ESC = Sdl.SDLK_ESCAPE;
    public const int KEY_UP = Sdl.SDLK_UP;
    public const int KEY_DOWN = Sdl.SDLK_DOWN;
    public const int KEY_LEFT = Sdl.SDLK_LEFT;
    public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public const int KEY_SPACE = Sdl.SDLK_SPACE;
    public const int KEY_INSERT = Sdl.SDLK_INSERT;
    public const int KEY_ENTER = Sdl.SDLK_RETURN;
    public const int KEY_TAB = Sdl.SDLK_TAB;
    public const int KEY_BACKSPACE = Sdl.SDLK_BACKSPACE;

    public const int KEY_0 = Sdl.SDLK_0;
    public const int KEY_1 = Sdl.SDLK_1;
    public const int KEY_2 = Sdl.SDLK_2;
    public const int KEY_3 = Sdl.SDLK_3;
    public const int KEY_4 = Sdl.SDLK_4;
    public const int KEY_5 = Sdl.SDLK_5;
    public const int KEY_6 = Sdl.SDLK_6;
    public const int KEY_7 = Sdl.SDLK_7;
    public const int KEY_8 = Sdl.SDLK_8;
    public const int KEY_9 = Sdl.SDLK_9;

    public const int KEY_A = Sdl.SDLK_a;
    public const int KEY_B = Sdl.SDLK_b;
    public const int KEY_C = Sdl.SDLK_c;
    public const int KEY_D = Sdl.SDLK_d;
    public const int KEY_E = Sdl.SDLK_e;
    public const int KEY_F = Sdl.SDLK_f;
    public const int KEY_G = Sdl.SDLK_g;
    public const int KEY_H = Sdl.SDLK_h;
    public const int KEY_I = Sdl.SDLK_i;
    public const int KEY_J = Sdl.SDLK_j;
    public const int KEY_K = Sdl.SDLK_k;
    public const int KEY_L = Sdl.SDLK_l;
    public const int KEY_M = Sdl.SDLK_m;
    public const int KEY_N = Sdl.SDLK_n;
    public const int KEY_O = Sdl.SDLK_o;
    public const int KEY_P = Sdl.SDLK_p;
    public const int KEY_Q = Sdl.SDLK_q;
    public const int KEY_R = Sdl.SDLK_r;
    public const int KEY_S = Sdl.SDLK_s;
    public const int KEY_T = Sdl.SDLK_t;
    public const int KEY_U = Sdl.SDLK_u;
    public const int KEY_V = Sdl.SDLK_v;
    public const int KEY_W = Sdl.SDLK_w;
    public const int KEY_X = Sdl.SDLK_x;
    public const int KEY_Y = Sdl.SDLK_y;
    public const int KEY_Z = Sdl.SDLK_z;

    public Sdl.SDL_Color Red { get; set; }
    public Sdl.SDL_Color White { get; set; }

    public IntPtr[] TextNums { get; set; }

    short screenWidth;
    short screenHeight;
    short colorDepth;

    int oldMouseX;
    int oldMouseY;

    static short startX, startY;

    IntPtr screen;

    public Hardware(short width, short height, short depth, bool fullScreen)
    {
        screenWidth = width;
        screenHeight = height;
        colorDepth = depth;

        Red = new Sdl.SDL_Color(255, 0, 0);
        White = new Sdl.SDL_Color(255, 255, 255);

        TextNums = new IntPtr[10];

        for (int i = 0; i < TextNums.Length; i++)
        {
            TextNums[i] = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/PermanentMarker-Regular.ttf", 20).GetFontType(),
                i.ToString(), Red);
        }

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags = flags | Sdl.SDL_FULLSCREEN;

        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        screen = Sdl.SDL_SetVideoMode(screenWidth, screenHeight, colorDepth, flags);
        Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_SetClipRect(screen, ref rect);

        SdlTtf.TTF_Init();
    }

    ~Hardware()
    {
        Sdl.SDL_Quit();
    }

    // Draws an image in its current coordinates
    public void DrawImage(Image img)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.ImageWidth,
            img.ImageHeight);
        Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
            img.ImageWidth, img.ImageHeight);
        Sdl.SDL_BlitSurface(img.ImagePtr, ref source, screen, ref target);
    }

    public void DrawEnemy(Enemy enemy)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, enemy.Width,
            enemy.Height);
        Sdl.SDL_Rect target = new Sdl.SDL_Rect(enemy.X, enemy.Y,
            enemy.Width, enemy.Height);
        Sdl.SDL_BlitSurface(enemy.EnemyImage.ImagePtr, ref source, screen, ref target);
    }

    // Draws a sprite from a sprite sheet in the specified X and Y position of the screen
    // The sprite to be drawn is determined by the x and y coordinates within the image, and the width and height to be cropped
    public void DrawSprite(Image image, short xScreen, short yScreen, short x, 
        short y, short width, short height)
    {
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, height);
        Sdl.SDL_BlitSurface(image.ImagePtr, ref src, screen, ref dest);
    }

    // Update screen
    public void UpdateScreen()
    {
        Sdl.SDL_Flip(screen);
    }

    // Detects if the user presses a key and returns the code of the key pressed
    public int KeyPressed()
    {
        int pressed = -1;

        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event keyEvent;
        if (Sdl.SDL_PollEvent(out keyEvent) == 1)
        {
            if (keyEvent.type == Sdl.SDL_KEYDOWN)
            {
                pressed = keyEvent.key.keysym.sym;
            }
        }

        return pressed;
    }

    // Checks if a given key is now being pressed
    public bool IsKeyPressed(int key)
    {
        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event evt;
        Sdl.SDL_PollEvent(out evt);
        int numKeys;
        byte[] keys = Sdl.SDL_GetKeyState(out numKeys);
        if (keys[key] == 1)
            pressed = true;
        return pressed;
    }

    // Clears the screen
    public void ClearScreen()
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_FillRect(screen, ref source, 0);
    }

    // Clears the bottom of the screen
    public void ClearBottom()
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, GameController.SCREEN_HEIGHT, screenWidth, (short)(screenHeight - GameController.SCREEN_HEIGHT));
        Sdl.SDL_FillRect(screen, ref source, 0);
    }

    // Clears the specified part of the screen
    public void ClearPart(short x, short y, short w, short h)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(x, y, w, h);
        Sdl.SDL_FillRect(screen, ref source, 0);
    }

    // Writes a text in the specified coordinates
    public void WriteText(string text, short x, short y, byte r, byte g, byte b,
                          Font fontType)
    {
        Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
        IntPtr textAsImage = SdlTtf.TTF_RenderText_Solid(fontType.GetFontType(),
            text, color);
        if (textAsImage == IntPtr.Zero)
            Environment.Exit(5);
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, screenWidth, screenHeight);
        Sdl.SDL_BlitSurface(textAsImage, ref src, screen, ref dest);
    }

    // Writes a text in the specified coordinates
    public void WriteText(IntPtr textAsImage, short x, short y)
    {
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, screenWidth, screenHeight);
        Sdl.SDL_BlitSurface(textAsImage, ref src, screen, ref dest);
    }

    // Writes a line in the specified coordinates, with the specified color and alpha
    public void DrawLine(short x, short y, short x2, short y2, byte r, byte g, byte b, byte alpha)
    {
        SdlGfx.lineRGBA(screen, x, y, x2, y2, r, g, b, alpha);
    }

    public void GetEvents(out int mouseX, out int mouseY,
        out int mouseClickX, out int mouseClickY)
    {
        mouseX = oldMouseX;
        mouseY = oldMouseY;
        mouseClickX = mouseClickY = -1;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event evt;
        if (Sdl.SDL_PollEvent(out evt) == 1)
        {
            if (evt.type == Sdl.SDL_MOUSEMOTION)
            {
                mouseX = evt.motion.x;
                mouseY = evt.motion.y;

                oldMouseX = mouseX;
                oldMouseY = mouseY;
            }
            if (evt.type == Sdl.SDL_MOUSEBUTTONDOWN)
            {
                mouseClickX = evt.motion.x;
                mouseClickY = evt.motion.y;
            }
        }
    }

    // Scroll Methods

    public static void ResetScroll()
    {
        startX = startY = 0;
    }

    public static void ScrollTo(short newStartX, short newStartY)
    {
        startX = newStartX;
        startY = newStartY;
    }

    public static void ScrollHorizontally(short xDespl)
    {
        startX += xDespl;
    }

    public static void ScrollVertically(short yDespl)
    {
        startY += yDespl;
    }
}
