using System.Linq;
using UnityEngine;

public class SpawningState : IWaveState, IPausable
{
    private readonly EnemyPoolManager _enemyPoolManager;
    private readonly Transform _spawnPoint;
    private readonly LvlWaves _lvlWaves;
    private readonly WaveManager _waveManager;
    private readonly WaveStateMachine _stateMachine;

    private float _waveDurationTimeRemaining;
    private float _spawnTimer;
    private int _enemiesSpawned;
    private float _lastUpdate;
    private bool _waveCompleted;
    private bool _isPaused;

    private const float UpdateInterval = 0.1f;

    public SpawningState(EnemyPoolManager enemyPoolManager, Transform spawnPoint, LvlWaves lvlWaves, WaveManager waveManager, WaveStateMachine stateMachine)
    {
        _enemyPoolManager = enemyPoolManager;
        _spawnPoint = spawnPoint;
        _lvlWaves = lvlWaves;
        _waveManager = waveManager;
        _stateMachine = stateMachine;
    }

    public float WaveDurationTimeRemaining
    {
        get => _waveDurationTimeRemaining;
        set => _waveDurationTimeRemaining = value;
    }

    public float SpawnTimer
    {
        get => _spawnTimer;
        set => _spawnTimer = value;
    }

    public int EnemiesSpawned
    {
        get => _enemiesSpawned;
        set => _enemiesSpawned = value;
    }

    public void Enter()
    {
        if (!_isPaused)
        {
            var wave = _lvlWaves.Wawes[_waveManager.CurrentWaveIndex];
            _enemiesSpawned = 0;
            _waveDurationTimeRemaining = wave.WaveUnits.Sum(unit => unit.delay);
            _spawnTimer = 0;
        }
        _lastUpdate = Time.time;
        _waveCompleted = false;
    }

    public void Update()
    {
        if (_isPaused) return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0 && _enemiesSpawned < _lvlWaves.Wawes[_waveManager.CurrentWaveIndex].WaveUnits.Length)
        {
            SpawnNextEnemy();
            _spawnTimer = CalculateNextSpawnDelay();
        }

        _waveDurationTimeRemaining -= Time.deltaTime;

        if (Time.time - _lastUpdate >= UpdateInterval)
        {
            float progress = 1 - (_waveDurationTimeRemaining / _lvlWaves.Wawes[_waveManager.CurrentWaveIndex].WaveUnits.Sum(unit => unit.delay));
            _stateMachine.NotifyWaveProgressChanged(progress);
            _lastUpdate = Time.time;
        }

        if (!_waveCompleted && _waveDurationTimeRemaining <= 0 && _enemiesSpawned >= _lvlWaves.Wawes[_waveManager.CurrentWaveIndex].WaveUnits.Length)
        {
            _waveManager.NotifyWaveCompletion();
            _waveCompleted = true;
        }
    }

    public void Exit() { }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
        _lastUpdate = Time.time;
    }

    private void SpawnNextEnemy()
    {
        if (_waveManager.CurrentWaveIndex >= _lvlWaves.Wawes.Length) return;

        Wave wave = _lvlWaves.Wawes[_waveManager.CurrentWaveIndex];
        if (_enemiesSpawned >= wave.WaveUnits.Length) return;

        WaveUnit waveUnit = wave.WaveUnits[_enemiesSpawned];
        Enemy enemy = _enemyPoolManager.GetEnemy(waveUnit.enemy);
        Debug.Log(waveUnit.enemy.ToString());
        Debug.Log(enemy.name);
        if (enemy != null)
        {
            enemy.transform.position = _spawnPoint.position;
            enemy.gameObject.SetActive(true);
            _enemiesSpawned++;
        }
    }

    private float CalculateNextSpawnDelay()
    {
        if (_waveManager.CurrentWaveIndex >= _lvlWaves.Wawes.Length)
            return 0;

        var wave = _lvlWaves.Wawes[_waveManager.CurrentWaveIndex];
        if (_enemiesSpawned >= wave.WaveUnits.Length)
            return 0;

        return wave.WaveUnits[_enemiesSpawned].delay;
    }
}
