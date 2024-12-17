using UnityEngine;
using UnityEngine.Pool;

public class ItemPool<T> where T : Item
{
    private readonly T _prefab;
    private readonly ObjectPool<T> _objectPool;

    public ItemPool(
        T prefab,
        bool collectionCheck = true,
        int defaultCapacity = 10,
        int maxSize = 10000)
    {
        _prefab = prefab;

        _objectPool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (item) => item.gameObject.SetActive(true),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize);
    }

    public T Get()
    {
        return _objectPool.Get();
    }

    public void Relise(Item item)
    {
        _objectPool.Release(item as T);
    }

    private T CreateFunc()
    {
        T item = MonoBehaviour.Instantiate(_prefab);
        item.gameObject.SetActive(false);
        return item;
    }
}