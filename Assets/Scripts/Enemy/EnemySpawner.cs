using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Bullet _bulletBrefabs;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _respawnTime;
    [SerializeField] private MainCollider _mainCollider;

    private SpawnPoint _lastSpawnPoint;
    private ItemPool<Enemy> _pool;
    private ItemPool<Bullet> _bulletPool;
    private List<Enemy> _livingEnemies;
    private List<Bullet> _livingBullets;
    private float _time;

    private void Start()
    {
        _time = _respawnTime;
        _pool = new ItemPool<Enemy>(_prefab);
        _bulletPool = new ItemPool<Bullet>(_bulletBrefabs);
        _livingEnemies = new List<Enemy>();
        _mainCollider.ItemCameOut += ReleaseEnemy;
    }

    public void ReleaseEnemy(PoolObject obj)
    {
        if (obj is Enemy enemy)
        {
            _livingEnemies.Remove(enemy);
            _pool.Relise(enemy);
        }
    }

    private void Update()
    {
        if (_time < 0f)
        {
            Spawn();
            _time = _respawnTime;
        }
        else
        {
            _time -= Time.deltaTime;
        }

        for (int i = 0; i < _livingEnemies.Count; i++)
        {
            _livingEnemies[i].transform.Translate(_speed * Time.deltaTime * Vector3.left);
        }
    }

    private void Spawn()
    {
        int totalSum = _lastSpawnPoint != null ? SumWeight() - _lastSpawnPoint.Weight : SumWeight();
        int randomNumber = UnityEngine.Random.Range(0, totalSum + 1);
        int currentSum = 0;

        SpawnPoint currentPoint = null;

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            currentPoint = _spawnPoints[i];

            if (currentPoint == _lastSpawnPoint)
                continue;

            currentSum += currentPoint.Weight;

            if (currentSum >= randomNumber)
            {
                Enemy enemy = _pool.Get();
                enemy.Initialization(this);
                enemy.Dead += ReleaseEnemy;
                enemy.transform.position = currentPoint.transform.position;
                _livingEnemies.Add(enemy);
                break;
            }

        }

        currentPoint.TakeWeight();
        _lastSpawnPoint = currentPoint;
    }

    private int SumWeight()
    {
        int result = 0;

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            result += _spawnPoints[i].Weight;
        }

        return result;
    }
}
