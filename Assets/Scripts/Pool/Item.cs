using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direction;

    public Action<Item> LeavingScreen;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _direction);
    }
}
