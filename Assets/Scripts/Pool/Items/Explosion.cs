using UnityEngine;

public class Explosion : Item
{
    [SerializeField, Min(0f)] private float _lifeTime;

    private float _currentLifeTime;

    private void OnEnable()
    {
        _currentLifeTime = _lifeTime;
    }

    private void Update()
    {
        _currentLifeTime -= Time.deltaTime;

        if (_currentLifeTime < 0)
        {
            LeavingScreen.Invoke(this);
        }
    }
}
