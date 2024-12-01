using System;
using UnityEngine;

public class Enemy : PoolObject
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    public Action<Enemy> Dead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);

        if (collision.gameObject.TryGetComponent<BirdMover>(out _))
        {
            ItemPool.ReleaseEnemy(this);
        }
        else if(collision.gameObject.TryGetComponent<Bullet>(out _))
        {
            ItemPool.ReleaseEnemy(this);
        }
    }

    
}

