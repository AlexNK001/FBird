using UnityEngine;

public class ScreenRepeater : MonoBehaviour
{
    [SerializeField] private float _screenWidth = 40f;
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Screen[] _screen;
    [SerializeField] private Game _game;

    private Vector3 _bias;

    private void Start()
    {
        _game.Restarted += Restart;
        _bias = new Vector3(_screenWidth, 0f, 0f);
    }


    private void Update()
    {
        for (int i = 0; i < _screen.Length; i++)
        {
            Transform currentTransform = _screen[i].transform;

            if (currentTransform.position.x < _minPositionY)
            {
                currentTransform.position = GetPositionRightmost() + _bias;
            }

            currentTransform.Translate(_speed * Time.deltaTime * Vector2.left);
        }
    }

    private void Restart()
    {
        foreach (Screen item in _screen)
        {
            item.transform.position = item.StartPosition;
        }
    }

    private Vector3 GetPositionRightmost()
    {
        Vector3 position = Vector3.zero;
        float currentMaxXPosition = float.MinValue;

        for (int i = 0; i < _screen.Length; i++)
        {
            Vector3 currentPosition = _screen[i].transform.position;

            if (currentPosition.x > currentMaxXPosition)
            {
                position = currentPosition;
                currentMaxXPosition = currentPosition.x;
            }
        }

        return position;
    }
}

