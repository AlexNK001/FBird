using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Timer _timer;

    private int _score;

    public event Action<int> ScoreChanged;

    private void Awake()
    {
        _timer.TimeHasCome += Add;
        _game.Restarted += Restart;
    }

    private void OnDestroy()
    {
        _timer.TimeHasCome -= Add;
        _game.Restarted -= Restart;
    }

    private void Add()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    private void Restart()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
