using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private BirdMover _player;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _restart;

    public Action Restarted;

    private void Start()
    {
        _player.Dead += Pause;
        _canvas.gameObject.SetActive(false);
        _restart.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        _player.Dead -= Pause;
        _restart.onClick.RemoveListener(Restart);
    }

    private void Update()
    {
        if(_canvas.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        _canvas.gameObject.SetActive(true);
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        _canvas.gameObject.SetActive(false);
        Restarted.Invoke();
    }
}
