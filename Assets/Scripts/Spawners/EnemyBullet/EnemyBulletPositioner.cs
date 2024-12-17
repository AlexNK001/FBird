using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class EnemyBulletPositioner : IPositioner, IUnsubscriber
{
    private readonly Timer _timer;
    private readonly Game _game;
    private readonly Spawner<Enemy> _enemySpwner;

    private readonly List<Item> _enemies;

    public EnemyBulletPositioner(Game game, Spawner<Enemy> enemySpawner, Timer timer)
    {
        _game = game;
        _enemySpwner = enemySpawner;
        _timer = timer;
        _enemies = new List<Item>();

        _game.Restarted += Clear;
        _enemySpwner.Getting += Add;
        _enemySpwner.Relesing += Remove;
        _timer.TimeHasCome += SelectShootingEnemy;
    }

    public event Action<Transform> SpawnPointAppeared;

    public void Unsubsribe()
    {
        _game.Restarted -= Clear;
        _enemySpwner.Getting -= Add;
        _enemySpwner.Relesing -= Remove;
        _timer.TimeHasCome -= SelectShootingEnemy;
    }

    private void Clear()
    {
        _enemies.Clear();
    }

    private void Add(Item item)
    {
        _enemies.Add(item);
    }

    private void Remove(Item item)
    {
        if (_enemies.Contains(item))
            _enemies.Remove(item);
    }

    private void SelectShootingEnemy()
    {
        if (_enemies.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _enemies.Count);
            Item enemy = _enemies[randomIndex];
            SpawnPointAppeared.Invoke(enemy.transform);
            _enemies.Remove(enemy);
        }
    }
}

