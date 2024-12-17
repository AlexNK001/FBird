using System;
using UnityEngine;

public class ExplosionPositioner : IPositioner, IUnsubscriber
{
    private readonly Spawner<Bullet> _spawner;

    public ExplosionPositioner(Spawner<Bullet> spawner)
    {
        _spawner = spawner;
        _spawner.Relesing += Spawn;
    }

    public event Action<Transform> SpawnPointAppeared;

    public void Unsubsribe()
    {
        _spawner.Relesing -= Spawn;
    }

    private void Spawn(Item item)
    {
        if (item is Bullet bullet && bullet.CameContactEnemy)
            SpawnPointAppeared.Invoke(bullet.transform);
    }
}
