using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PoolObject : MonoBehaviour 
{
    protected EnemySpawner ItemPool;

    public virtual void Initialization(EnemySpawner enemySpawner)
    {
        ItemPool = enemySpawner;
    }
}
