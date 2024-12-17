using System;
using UnityEngine;

public class BirdMover : MonoBehaviour, IPositioner
{
    [SerializeField] private Game _game;
    [SerializeField] private float _tapForse;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _shootPoint;

    private Vector3 _startPosition;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    public Action Dead;
    public Action<Vector2, Quaternion> Shoot;

    public event Action<Transform> SpawnPointAppeared;

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnPointAppeared.Invoke(_shootPoint);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rigidbody.velocity = new Vector2(0f, _tapForse);
            transform.rotation = _maxRotation;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<KillingCollider>(out _))
        {
            Dead.Invoke();
        }
    }

    private void Restart()
    {
        transform.position = _startPosition;
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
