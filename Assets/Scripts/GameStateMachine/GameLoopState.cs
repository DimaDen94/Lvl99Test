public class GameLoopState : IGameState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly Hero _hero;
    private readonly StructureManager _structureManager;
    private readonly WaveManager _waveManager;
    private readonly IWalletService _wallet;
    private readonly MainMenuMediator _mainMenuMediator;
    private readonly EnemyPoolManager _enemyPoolManager;

    public GameLoopState(GameStateMachine gameStateMachine, Hero hero, StructureManager structureManager, WaveManager waveManager, EnemyPoolManager enemyPoolManager, IWalletService wallet, MainMenuMediator mainMenuMediator)
    {
        _gameStateMachine = gameStateMachine;
        _hero = hero;
        _structureManager = structureManager;
        _waveManager = waveManager;
        _enemyPoolManager = enemyPoolManager;
        _wallet = wallet;
        _mainMenuMediator = mainMenuMediator;
    }

    public void Enter()
    {
        _structureManager.Activate();
        _enemyPoolManager.EnemyDied += OnEnemyDied;
        _waveManager.OnAllWavesCompleted += OnVictory;
        _hero.OnDeath += OnDefeat;
        _hero.OnLifeLost += OnLifeLost;
    }

    public void Exit()
    {
        _structureManager.Deactivate();
        _enemyPoolManager.EnemyDied -= OnEnemyDied;
        _waveManager.OnAllWavesCompleted -= OnVictory;
        _hero.OnDeath -= OnDefeat;
        _hero.OnLifeLost -= OnLifeLost;
    }

    private void OnLifeLost(int lifeLeft)
    {
        _mainMenuMediator.UpdateLifes(lifeLeft);
    }

    private void OnVictory()
    {
        _gameStateMachine.Enter<VictoryState>();
    }

    private void OnDefeat()
    {
        _gameStateMachine.Enter<DefeatState>();
    }

    private void OnEnemyDied(int reward)
    {
        _wallet.AddCoins(reward);
    }

    public void Update()
    {
        _waveManager.Update();
    }
}
