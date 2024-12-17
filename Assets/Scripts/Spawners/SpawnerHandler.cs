using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private BirdMover _player;
    [Header("Prefabs")]
    [SerializeField] private Bullet _playerBullet;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private Explosion _explosion;
    [Header("Positioners")]
    [SerializeField] private EnemyPositioner _enemyPositioner;
    [Header("Timers")]
    [SerializeField] private Timer _enemyShootingTimer;

    private Spawner<Bullet> _playerBulletSpawner;
    private ExplosionPositioner _explosionPositioner;
    private Spawner<Explosion> _explosionSpawner;
    private Spawner<Enemy> _enemySpawner;
    private Spawner<Bullet> _enemyBulletSpawner;
    private EnemyBulletPositioner _enemyBulletPositioner;

    private IUnsubscriber[] _unsubscribers;

    private void Start()
    {
        _playerBulletSpawner = new Spawner<Bullet>(_game, _player, _playerBullet);
        _explosionPositioner = new ExplosionPositioner(_playerBulletSpawner);
        _explosionSpawner = new Spawner<Explosion>(_game, _explosionPositioner, _explosion);
        _enemySpawner = new Spawner<Enemy>(_game, _enemyPositioner, _enemy);
        _enemyBulletPositioner = new EnemyBulletPositioner(_game, _enemySpawner, _enemyShootingTimer);
        _enemyBulletSpawner = new Spawner<Bullet>(_game, _enemyBulletPositioner, _enemyBullet);

        _unsubscribers = new IUnsubscriber[]
        {
            _playerBulletSpawner,
            _explosionPositioner,
            _explosionSpawner,
            _enemySpawner,
            _enemyBulletSpawner,
            _enemyBulletPositioner
        };
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _unsubscribers.Length; i++)
        {
            _unsubscribers[i].Unsubsribe();
        }
    }
}
