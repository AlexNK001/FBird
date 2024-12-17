using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BirdMover _player;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private StartScreen _startScreen;

    public Action Restarted;

    private void Start()
    {
        _endGameScreen.gameObject.SetActive(false);
        Time.timeScale = 0f;

        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _player.Dead += OnGameOver;
    }

    private void OnDestroy()
    {
        _player.Dead -= Pause;
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _player.Dead -= OnGameOver;
    }

    private void OnGameOver()
    {
        _endGameScreen.gameObject.SetActive(true);
        Pause();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.gameObject.SetActive(false);
        Restart();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.gameObject.SetActive(false);
        Restart();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        //Restarted.Invoke();
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        Restarted.Invoke();
    }
}

