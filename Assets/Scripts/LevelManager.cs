using UnityEngine;

public class LevelManager : Sounds
{ 
    [SerializeField] private Rigidbody _player;
    [SerializeField] private GameplayHUD _gameplayHUD;

    private Transform _spawnPoint;
    private Transform _lastPoint;
    private Transform _playerTransform;

    #region MONO

    private void OnEnable()
    {
        Checkpoint.OnCheckpointReached += NewSpawnPoint;
    }

    private void OnDisable()
    {
        Checkpoint.OnCheckpointReached += NewSpawnPoint;
    }

    #endregion

    private void Awake()
    {
        _spawnPoint = _player.transform;
        _playerTransform = _player.transform;
    }

    public void Respawn()
    {
        if (_spawnPoint != null)
        {
            _playerTransform.position = _spawnPoint.position;
            _playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            _player.velocity = Vector3.zero;
            _lastPoint = _spawnPoint;
            _spawnPoint = null;
            PlaySound(sounds[0], 1f, false, 0.97f, 1.1f);

        }
        else
        {
            _gameplayHUD.Lose(_lastPoint);
            StopSound(sounds[1]);
        }
    }

    public void NewSpawnPoint(Transform newPoint)
    {
        _spawnPoint = newPoint;
    }

    public void LastChance(Transform newPoint)
    {
        _playerTransform.position = newPoint.transform.position;
        _playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        _player.velocity = Vector3.zero;
    }
}