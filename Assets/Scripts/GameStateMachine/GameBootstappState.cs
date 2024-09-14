using System;

public class GameBootstappState : IGameState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly WaveManager _waveManager;
    private readonly MainMenuMediator _mainMenuMediator;
    private readonly IWalletService _wallet;
    private readonly SessionTimer _sessionTimer;

    public GameBootstappState(GameStateMachine gameStateMachine, WaveManager waveManager, MainMenuMediator mainMenuMediator, IWalletService wallet, SessionTimer sessionTimer)
    {
        _gameStateMachine = gameStateMachine;
        _waveManager = waveManager;
        _mainMenuMediator = mainMenuMediator;
        _wallet = wallet;
        _sessionTimer = sessionTimer;
    }

    public void Enter()
    {
        _waveManager.StartWaves();
        _wallet.SetStartCoins(initialCoins: 35);
        _mainMenuMediator.Construct(_wallet, _waveManager, _gameStateMachine);
        _sessionTimer.OnTimeUpdated += UpdateTimer;
        _sessionTimer.Start();
        _gameStateMachine.Enter<GameLoopState>();
    }

    private void UpdateTimer(int time)
    {
        _mainMenuMediator.UpdateTime(time);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}