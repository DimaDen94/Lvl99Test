using System;
using System.Collections;
using UnityEngine;

public class SessionTimer : IPausable
{
    private readonly ICoroutineRunner _coroutineRunner;
    private Coroutine _timerCoroutine;
    private bool _isPaused;
    private int _elapsedTime;

    public event Action<int> OnTimeUpdated;

    public SessionTimer(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _elapsedTime = 0;
    }

    public void Start()
    {
        if (_timerCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_timerCoroutine);
        }

        _timerCoroutine = _coroutineRunner.StartCoroutine(TimerCoroutine());
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        if (_isPaused)
        {
            _isPaused = false;
            _timerCoroutine = _coroutineRunner.StartCoroutine(TimerCoroutine());
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (!_isPaused)
        {
            _elapsedTime += 1;
            OnTimeUpdated?.Invoke(_elapsedTime);

            yield return new WaitForSeconds(1f);
        }
    }
}