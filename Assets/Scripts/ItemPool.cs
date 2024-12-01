using System;
using UnityEngine;
using UnityEngine.Pool;

public class ItemPool<T> where T : PoolObject
{
    private T _prefab;
    private ObjectPool<T> _objectPool;

    public ItemPool(T prefab, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
    {
        _prefab = prefab;

        _objectPool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (t) => ActionOnGet(t),
            actionOnRelease: (t) => ActionOnRelease(t),
            actionOnDestroy: (t) => ActionOnDestroy(t),
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize);
    }

    public T Get()
    {
        return _objectPool.Get();
    }

    public void Relise(T t)
    {
        _objectPool.Release(t);
    }

    private T CreateFunc()
    {
        T enemy = MonoBehaviour.Instantiate(_prefab);
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    private void ActionOnGet(T t)
    {
        t.gameObject.SetActive(true);
    }

    private void ActionOnRelease(T t)
    {
        t.gameObject.SetActive(false);
    }

    private bool ActionOnDestroy(T t)
    {
        throw new System.NotImplementedException();
    }
}