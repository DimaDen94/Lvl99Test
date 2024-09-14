using System;
using UnityEngine;

public class WaveStateMachine
{
    public IWaveState WaitingState { get; private set; }
    public IWaveState SpawningState { get; private set; }
    public IWaveState AllWavesCompletedState { get; private set; }

    public IWaveState CurrentState { get; private set; }

    public event Action<IWaveState> OnStateChanged;
    public event Action<float> OnWaveProgressChanged;
    public event Action<float> OnPauseTimeRemainingChanged;

    public WaveStateMachine(
        float waveDelay,
        EnemyPoolManager enemyPoolManager,
        Transform spawnPoint,
        LvlWaves lvlWaves,
        WaveManager waveManager,
        ActiveEnemyHolder activeEnemyHolder)
    {
        WaitingState = new WaitingState(waveDelay, this);
        SpawningState = new SpawningState(enemyPoolManager, spawnPoint, lvlWaves, waveManager, this);
        AllWavesCompletedState = new AllWavesCompletedState(waveManager, activeEnemyHolder);

        CurrentState = WaitingState;
    }

    public void TransitionToState(IWaveState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
        CurrentState.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void NotifyWaveProgressChanged(float progress)
    {
        OnWaveProgressChanged?.Invoke(progress);
    }

    public void NotifyPauseTimeRemaining(float timeRemaining)
    {
        OnPauseTimeRemainingChanged?.Invoke(timeRemaining);
    }
}

