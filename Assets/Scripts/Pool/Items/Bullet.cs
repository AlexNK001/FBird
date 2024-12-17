using UnityEngine;

public class Bullet : Item
{
    public bool CameContactEnemy { get; private set; }

    private void OnEnable()
    {
        CameContactEnemy = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CameContactEnemy = collision.transform.TryGetComponent<Enemy>(out _);
        LeavingScreen.Invoke(this);
    }
}
