using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IGameState> _states;
    private IGameState _currentState;

    public GameStateMachine(Hero hero, WaveManager waveManager, IWalletService wallet, StructureManager structureManager, EnemyPoolManager enemyPoolManager, UIFactory uIFactory,
        ActiveEnemyHolder activeEnemyHolder, StructureHolder structureHolder, MainMenuMediator mainMenuMediator, SessionTimer sessionTimer)
    {
        _states = new Dictionary<Type, IGameState>
        {
            { typeof(GameBootstappState), new GameBootstappState(this, waveManager, mainMenuMediator, wallet,sessionTimer)},
            { typeof(GameLoopState), new GameLoopState(this, hero, structureManager, waveManager,enemyPoolManager, wallet, mainMenuMediator)},
            { typeof(PauseState), new PauseState(this,uIFactory, activeEnemyHolder, structureHolder, waveManager, hero, sessionTimer)},
            { typeof(VictoryState), new VictoryState(uIFactory) },
            { typeof(DefeatState), new DefeatState(uIFactory) }
        };
    }

    public void Enter<TState>() where TState : class, IGameState
    {
        _currentState?.Exit();
        _currentState = ChangeState<TState>();
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    private IGameState ChangeState<TState>() where TState : class, IGameState
    {
        return _states[typeof(TState)];
    }
}
