/*
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal:
 */

using System;
using System.Collections.Generic;
using Tao.Sdl;

class GameScreen : Screen
{
    protected Enemy enemy; // Test enemy.
    protected Level level;
    protected Character character;
    protected Font font;
    protected short xMap;
    protected short yMap;

    public GameScreen(Hardware hardware) : base(hardware)
    {
        font = new Font("fonts/PermanentMarker-Regular.ttf", 20);
        level = new Level("levels/level1.txt");
        character = new Character(300);
        xMap = 0;
        yMap = 0;

        enemy = new Enemy(100, 25); // Test enemy.
    }

    private void moveCharacter()
    {
        short moveUp = 0;
        short moveRight = 1;
        short moveDown = 2;
        short moveLeft = 3;

        bool left = hardware.IsKeyPressed(Hardware.KEY_LEFT);
        bool right = hardware.IsKeyPressed(Hardware.KEY_RIGHT);
        bool up = hardware.IsKeyPressed(Hardware.KEY_UP);
        bool down = hardware.IsKeyPressed(Hardware.KEY_DOWN);

        if (up)
        {
            level.MoveObstacles(moveUp, character.Speed);
            yMap--;
        }
        if (right)
        {
            xMap++;
            level.MoveObstacles(moveRight, character.Speed);
        }
        if (down)
        {
            yMap++;
            level.MoveObstacles(moveDown, character.Speed);
        }
        if (left)
        {
            xMap--;
            level.MoveObstacles(moveLeft, character.Speed);
        }
    }

    public override void Show()
    {
        short centeredCharacterX =
            (short)((GameController.SCREEN_WIDTH / 2) - (character.Width / 2));
        short centeredCharacterY =
           (short)((GameController.SCREEN_HEIGHT / 2) - (character.Height / 2));

        character.character.MoveTo(centeredCharacterX, centeredCharacterY);
        character.MoveTo(centeredCharacterX, centeredCharacterY);

        enemy.MoveTo(400, 400);
        enemy.enemy.MoveTo(400, 400);

        short oldX, oldY, oldXMap, oldYMap;

        do
        {
            if (character.Life > 0)
            {
                // TODO: 1. Draw everything.
                hardware.UpdateScreen();

                hardware.DrawSprite(level.Floor, 0, 0, level.XMap,
                    level.YMap, GameController.SCREEN_WIDTH,
                    GameController.SCREEN_HEIGHT);

                level.DrawObstacles(hardware);
                hardware.DrawImage(character.character);
                hardware.DrawImage(enemy.enemy);
                hardware.UpdateScreen();
                // TODO: 2. Move character from keyboard input.
                oldX = character.X;
                oldY = character.Y;
                oldXMap = level.XMap;
                oldYMap = level.YMap;
                moveCharacter();
                
                // TODO: 3. Move enemies and objects.
                enemy.GoToPlayer(character);

                // TODO: 4. Check collisions and update game state.
                if (enemy.CharacterIsOnRange(character))
                    enemy.Attack(character);
            }
        }
        while (character.Life > 0 && !hardware.IsKeyPressed(Hardware.KEY_ESC));
    }
}
