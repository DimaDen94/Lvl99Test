using UnityEngine;

public class AllWavesCompletedState : IWaveState
{
    private readonly WaveManager _waveManager;
    private readonly ActiveEnemyHolder _activeEnemyHolder;
    private float _checkInterval = 1f;
    private float _lastCheckTime;

    public AllWavesCompletedState(WaveManager waveManager, ActiveEnemyHolder activeEnemyHolder)
    {
        _waveManager = waveManager;
        _activeEnemyHolder = activeEnemyHolder;
    }

    public void Enter()
    {
        _lastCheckTime = Time.time;
    }

    public void Update()
    {
        if (Time.time - _lastCheckTime >= _checkInterval)
        {
            if (_activeEnemyHolder.GetActiveEnemies().Count == 0)
            {
                _waveManager.NotifyAllEnemyDeath();
            }
            _lastCheckTime = Time.time;
        }
    }

    public void Exit() { }
}

