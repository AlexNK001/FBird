using System;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyPositioner : MonoBehaviour, IPositioner
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Timer _timer;

    private SpawnPoint _lastSpawnPoint;

    public event Action<Transform> SpawnPointAppeared;

    private void Start()
    {
        _timer.TimeHasCome += Spawn;
    }

    private void OnDestroy()
    {
        _timer.TimeHasCome -= Spawn;
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
                SpawnPointAppeared.Invoke(currentPoint.transform);
                break;
            }
        }

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
