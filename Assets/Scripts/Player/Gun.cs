using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private BirdMover _birdMover;
    [SerializeField] private float _speed;

    [SerializeField] private List<Bullet> _bullets;

    private void Start()
    {
        _bullets = new List<Bullet>();
        _birdMover.Shoot += Shoot;
    }

    private void Shoot(Quaternion obj)
    {
        Bullet bullet = Instantiate(_prefab);
        bullet.transform.position = _bulletSpawnPosition.position;
        bullet.transform.rotation = obj;
        _bullets.Add(bullet);
    }

    private void Update()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            Transform bullet = _bullets[i].transform;
            bullet.Translate(bullet.right * _speed * Time.deltaTime);
        }
    }
}
