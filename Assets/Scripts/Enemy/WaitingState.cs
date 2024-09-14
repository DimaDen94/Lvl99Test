using UnityEngine;

public class WaitingState : IWaveState, IPausable
{
    private readonly float _waveDelay;
    private readonly WaveStateMachine _stateMachine;

    private float _waveDelayTimeRemaining;
    private float _lastUpdate;
    private bool _isPaused;

    private const float UpdateInterval = 0.1f;

    public WaitingState(float waveDelay, WaveStateMachine stateMachine)
    {
        _waveDelay = waveDelay;
        _stateMachine = stateMachine;
    }

    public float WaveDelayTimeRemaining
    {
        get => _waveDelayTimeRemaining;
        set => _waveDelayTimeRemaining = value;
    }

    public void Enter()
    {
        if (!_isPaused)
        {
            _waveDelayTimeRemaining = _waveDelay;
            _lastUpdate = Time.time;
        }
    }

    public void Update()
    {
        if (_isPaused) return;

        _waveDelayTimeRemaining -= Time.deltaTime;

        if (Time.time - _lastUpdate >= UpdateInterval)
        {
            _stateMachine.NotifyPauseTimeRemaining(_waveDelayTimeRemaining);
            _lastUpdate = Time.time;
        }

        if (_waveDelayTimeRemaining <= 0)
        {
            _stateMachine.TransitionToState(_stateMachine.SpawningState);
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
}
