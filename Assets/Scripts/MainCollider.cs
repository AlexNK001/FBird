using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MainCollider : MonoBehaviour
{
    public Action<PoolObject> ItemCameOut;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("!");

        if (collision.TryGetComponent(out PoolObject gameItem))
        {
            //Debug.Log(gameItem.name);
            ItemCameOut.Invoke(gameItem);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("private void OnTriggerEnter2D(Collider2D collision)");
    }
}
