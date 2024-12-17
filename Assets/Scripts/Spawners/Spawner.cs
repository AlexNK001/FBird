using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : IUnsubscriber where T : Item
{
    private readonly ItemPool<T> _pool;
    private readonly Game _game;
    private readonly IPositioner _positionGenerator;
    private readonly List<T> _items;

    public Action<Item> Getting;
    public Action<Item> Relesing;

    public Spawner(Game game, IPositioner positionGenerator, T prefab)
    {
        _game = game;
        _game.Restarted += UnspawnAll;
        _positionGenerator = positionGenerator;
        _positionGenerator.SpawnPointAppeared += Spawn;
        _pool = new ItemPool<T>(prefab);
        _items = new List<T>();
    }

    private void UnspawnAll()
    {
        int itemsCount = _items.Count;

        for (int i = itemsCount - 1; i >= 0; i--)
        {
            Unspawn(_items[i]);
        }
    }

    public void Unsubsribe()
    {
        _positionGenerator.SpawnPointAppeared -= Spawn;
        _game.Restarted -= UnspawnAll;

        for (int i = 0; i < _items.Count; i++)
            _items[i].LeavingScreen -= Unspawn;
    }

    private void Spawn(Transform transform)
    {
        T item = _pool.Get();
        item.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
        item.LeavingScreen += Unspawn;
        Getting?.Invoke(item);
        _items.Add(item);
    }

    private void Unspawn(Item item)
    {
        T derivedItem = item as T;
        derivedItem.LeavingScreen -= Unspawn;
        Relesing?.Invoke(derivedItem);
        _items.Remove(derivedItem);
        _pool.Relise(derivedItem);
    }
}
