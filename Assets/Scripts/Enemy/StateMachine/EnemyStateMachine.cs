using System;
using System.Collections.Generic;
using UnityEngine.Splines;

public class EnemyStateMachine
{
    private IEnemyState _currentState;
    private Enemy _enemy;

    private Dictionary<Type, IEnemyState> _states = new Dictionary<Type, IEnemyState>();

    public EnemyStateMachine(Enemy enemy, Spline spline, Hero hero)
    {
        _enemy = enemy;

        _states[typeof(EnemyMoveState)] = new EnemyMoveState(_enemy, spline);
        _states[typeof(EnemyAttackState)] = new EnemyAttackState(_enemy, hero);
        _states[typeof(EnemyDeathState)] = new EnemyDeathState(_enemy);
        _states[typeof(EnemyCompletedPathState)] = new EnemyCompletedPathState(_enemy);
    }

    public void Enter<TState>() where TState : class, IEnemyState
    {
        if (_states.TryGetValue(typeof(TState), out IEnemyState newState))
        {
            SetState(newState);
            newState.Enter();
        }
        else
        {
            throw new InvalidOperationException($"State of type {typeof(TState)} is not registered.");
        }
    }

    private void SetState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
    }

    public void Update()
    {
        _currentState?.Execute();
    }
}
