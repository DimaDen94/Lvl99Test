using System;
using UnityEngine;

public class WaveManager : IPausable
{
    private readonly float _waveDelay = 10f;
    private readonly EnemyPoolManager _enemyPoolManager;
    private readonly Transform _spawnPoint;
    private readonly LvlWaves _lvlWaves;
    private WaveStateMachine _stateMachine;
    private int _currentWaveIndex = 0;

    public float WaveDelay => _waveDelay;
    public int CurrentWaveIndex => _currentWaveIndex;

    public event Action OnAllWavesCompleted;
    public event Action<float> OnCurrentWaveProgressChanged;
    public event Action<float> OnPauseTimeRemainingChanged;
    public event Action<IWaveState> OnStateChanged;

    public WaveManager(EnemyPoolManager enemyPoolManager, Transform spawnPoint, LvlWaves lvlWaves, ActiveEnemyHolder activeEnemyHolder)
    {
        _enemyPoolManager = enemyPoolManager;
        _spawnPoint = spawnPoint;
        _lvlWaves = lvlWaves;

        _stateMachine = new WaveStateMachine(
            _waveDelay,
            _enemyPoolManager,
            _spawnPoint,
            _lvlWaves,
            this,
            activeEnemyHolder
        );

        _stateMachine.OnStateChanged += HandleStateChanged;
        _stateMachine.OnWaveProgressChanged += HandleWaveProgressChanged;
        _stateMachine.OnPauseTimeRemainingChanged += HandlePauseTimeRemainingChanged;

        OnStateChanged?.Invoke(_stateMachine.CurrentState);
    }

    public void NotifyAllEnemyDeath()
    {
        OnAllWavesCompleted?.Invoke();
    }

    public void StartWaves()
    {
        _currentWaveIndex = 0;
        _stateMachine.TransitionToState(_stateMachine.WaitingState);
    }

    public void Pause()
    {
        if (_stateMachine.CurrentState is IPausable pausableState)
        {
            pausableState.Pause();
        }
    }

    public void Resume()
    {
        if (_stateMachine.CurrentState is IPausable pausableState)
        {
            pausableState.Resume();
        }
    }

    public void Update()
    {
        _stateMachine.Update();
    }

    public void NotifyWaveCompletion()
    {
        _currentWaveIndex++;
        if (_currentWaveIndex < _lvlWaves.Wawes.Length)
        {
            _stateMachine.TransitionToState(_stateMachine.WaitingState);
        }
        else
        {
            _stateMachine.TransitionToState(_stateMachine.AllWavesCompletedState);
        }
    }

    private void HandleStateChanged(IWaveState state)
    {
        OnStateChanged?.Invoke(state);
    }

    private void HandleWaveProgressChanged(float progress)
    {
        OnCurrentWaveProgressChanged?.Invoke(progress);
    }

    private void HandlePauseTimeRemainingChanged(float timeRemaining)
    {
        OnPauseTimeRemainingChanged?.Invoke(timeRemaining);
    }
}
