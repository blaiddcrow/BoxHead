﻿/*
 * * V.0.0.7 22/05/2018 - Javier Saorín Vidal: Added list of enemies and spawns,
 * created methods to add enemies with different stats and methods to put them
 * in random spawns.
**/
using System;
using System.Collections.Generic;

class EnemyGenerator
{
    public List<Enemy> enemies { get; set; }
    public List<SpawnPoint> spawnPoints { get; set; }
    private int amountOfEnemies { get; set; }
    private Random random;
    private Level level;
    private Hardware hardware;

    public EnemyGenerator(Hardware hardware)
    {
        this.hardware = hardware;
        enemies = new List<Enemy>();
        spawnPoints = new List<SpawnPoint>();
        amountOfEnemies = 0;
        random = new Random();
        level = new Level();
        level.Load();
        initialiceSpawns();
    }

    public void DeleteEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public void InitialiceRound(int round)
    {
        amountOfEnemies = round * random.Next(1, 5);

        for (int i = 0; i < amountOfEnemies; i++)
        {
            double life = random.NextDouble() * 100 + random.Next(50, 100);
            double damage = random.NextDouble() * 25 + random.Next(0, 25);
            short speed = 
                (short)((short)(round / 10) + (short)(random.Next(0, 1)));

            enemies.Add(new Enemy(life, damage, speed));
        }
    }

    private void initialiceSpawns()
    {
        SpawnPoint comparableSpawn = new SpawnPoint(0, 0);

        foreach (Obstacle obstacle in level.Obstacles)
        {
            string type = obstacle.ToString().Split(';')[0];
            if (type.ToLower() == "s")
                spawnPoints.Add((SpawnPoint)obstacle);
        }
    }

    public void SetEnemiesSpawnpoints()
    {
        initialiceSpawns();
        SpawnPoint randomSpawn;
        foreach (Enemy enemy in enemies)
        {
            int spawnNumber = random.Next(0, spawnPoints.Count - 1);
            randomSpawn = spawnPoints[spawnNumber];
            enemy.MoveTo(randomSpawn.X, randomSpawn.Y);
            enemy.EnemyImage.MoveTo(randomSpawn.X, randomSpawn.Y);
        }
    }
}