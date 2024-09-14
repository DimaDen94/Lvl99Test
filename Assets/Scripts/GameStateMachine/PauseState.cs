using UnityEngine;

public class PauseState : IGameState
{
    private readonly GameStateMachine _stateMachine;
    private readonly UIFactory _uiFactory;
    private readonly IPausable[] _pausables;
    private PauseScreen _screen;

    public PauseState(GameStateMachine stateMachine, UIFactory uiFactory, params IPausable[] pausables)
    {
        _stateMachine = stateMachine;
        _uiFactory = uiFactory;
        _pausables = pausables;
    }

    public void Enter()
    {
        foreach (var item in _pausables)
        {
            item.Pause();
        }

        GameObject gameObject = _uiFactory.CreatePauseDialog();
        _screen = gameObject.GetComponent<PauseScreen>();
        _screen.Play += Play;
    }

    private void Play()
    {
        _stateMachine.Enter<GameLoopState>();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        foreach (var item in _pausables)
        {
            item.Resume();
        }
        _screen.Play -= Play;
        UnityEngine.Object.Destroy(_screen.gameObject);
        _screen = null;

    }
}