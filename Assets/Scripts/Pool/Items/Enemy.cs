using UnityEngine;

public class Enemy : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LeavingScreen.Invoke(this);
    }
}
