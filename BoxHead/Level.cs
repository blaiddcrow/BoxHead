/*
 * * V.0.0.3 16/05/2018 - Javier Saorín Vidal: Implemented a DrawObstacle method
 * that draws all the obstacles in the screen.
 * V.0.0.2 15/05/2018 - Javier Saorín Vidal: Added a Load method that reads 
 * the type of the obstacle and his position from a text file.
 * Implemented methods to add diferent obstacles. Added a method to detect if
 * the character collides with a healthpack.
 * V.0.0.1 14/05/2018 - Javier Saorín Vidal: Created the basic skelleton of 
 * this class.
 */

using System;
using System.Collections.Generic;
using System.IO;

class Level
{
    public const int MAP_WIDTH = 2560;
    public const int MAP_HEIGHT = 1440;

    public List<Obstacle> Obstacles { get; }
    public List<HealthPack> Healthpacks { get; }
    public List<Enemy> Enemies { get; }

    public Image Floor { get; set; }
    public short Width { get; set; }
    public short Height { get; set; }
    public short XMap { get; set; }
    public short YMap { get; set; }
    public string ActualLevel { get; set; }

    public Level()
    {
        Obstacles = new List<Obstacle>();
        Healthpacks = new List<HealthPack>();
        Enemies = new List<Enemy>();
        Floor = new Image("imgs/others/floor.png", 1280, 720);
        XMap = YMap = 0;
    }

    public void DrawObstacles(Hardware hardware)
    {
        foreach (Obstacle o in Obstacles)
        {
            hardware.DrawSprite(o.Image, (short)(o.X - XMap), (short)(o.Y - YMap), 0, 0,
                   GameController.SCREEN_WIDTH, GameController.SCREEN_HEIGHT);
        }
    }

    public void MoveObstacles(int direction, short speed)
    {
        switch (direction)
        {
            case 0: // Up to down
                foreach (Obstacle o in Obstacles)
                    o.MoveTo(o.X, (short)(o.Y + speed));
                break;
            case 1: // Right to left
                foreach (Obstacle o in Obstacles)
                    o.MoveTo((short)(o.X - speed), o.Y);
                break;
            case 2: // Down to up
                foreach (Obstacle o in Obstacles)
                    o.MoveTo(o.X, (short)(o.Y - speed));
                break;
            case 3: // Left to right
                foreach (Obstacle o in Obstacles)
                    o.MoveTo((short)(o.X + speed), o.Y);
                break;
            default:
                break;
        }
    }

    public void Load()
    {
        if (File.Exists(ActualLevel))
        {
            try
            {
                StreamReader input = new StreamReader(ActualLevel);
                string line;
                do
                {
                    line = input.ReadLine();
                    if (line != null)
                    {
                        string[] blockData = line.Split(';');

                        string obstacleType = blockData[0].ToLower();
                        short obstacleX = short.Parse(blockData[1]);
                        short obstacleY = short.Parse(blockData[2]);

                        switch (blockData[0].ToLower())
                        {
                            case "w": // Wall block.
                                Obstacles.Add(new Wall(obstacleX, obstacleY));
                                break;
                            case "b": // Barrel.
                                Obstacles.Add(new Barrel(obstacleX, obstacleY));
                                break;
                            case "m": // Mine.
                                Obstacles.Add(new Mine(obstacleX, obstacleY));
                                break;
                            case "s": // Spawn point.
                                Obstacles.Add(new SpawnPoint(obstacleX, obstacleY));
                                break;
                            default:
                                break;
                        }
                    }

                } while (line != null);
                input.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path too long exception!");
            }
            catch (IOException)
            {
                Console.WriteLine("Input/Output exception!");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }

    public void AddWall(Wall w) { Obstacles.Add(w); }

    public void AddBarrel(Barrel b) { Obstacles.Add(b); }

    public void AddMine(Mine m) { Obstacles.Add(m); }

    public void AddWall(SpawnPoint s) { Obstacles.Add(s); }

    public bool CollidesCharacterWithHealthpack(Character character)
    {
        int pos = 0;
        bool collided = false;
        while (pos < Healthpacks.Count && !collided)
        {
            if (character.CollidesWith(Healthpacks[pos]))
            {
                collided = true;
                Healthpacks.RemoveAt(pos);
            }
            pos++;
        }
        return collided;
    }
}
