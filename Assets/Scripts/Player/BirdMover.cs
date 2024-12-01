using System;
using UnityEngine;

public class BirdMover : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private float _tapForse;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Vector3 _startPosition;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    public Action Dead;
    public Action<Quaternion> Shoot;

    private void Start()
    {
        _game.Restarted += Restart;

        _startPosition = transform.position;

        _maxRotation = Quaternion.Euler(0f, 0f, _maxRotationZ);
        _minRotation = Quaternion.Euler(0f, 0f, _minRotationZ);
    }

    private void OnDestroy()
    {
        _game.Restarted -= Restart;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Shoot.Invoke(transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(0f, _tapForse);
            transform.rotation = _maxRotation;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<KillingCollider>(out _))
        {
            Dead.Invoke();
        }
    }

    private void Restart()
    {
        transform.position = _startPosition;
        _rigidbody.velocity = Vector3.zero;
    }
}
