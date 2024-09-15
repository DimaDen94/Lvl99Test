using System;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour, IPausable
{
    public event Action Died;
    public event Action CompletedPath;

    public float AttackRange = 0.1f;

    private EnemyStateMachine stateMachine;
    private Hero _hero;

    public EnemyStateMachine StateMachine => stateMachine;

    public int Damage => _damage;
    public float Speed => _speed;
    public int Reward => _reward;

    private EnemyData _data;
    private float _health;
    private int _damage;
    private float _speed;
    private int _reward;

    private bool _hasAttacked;
    private bool _pause = false;

    public void Init(EnemyData data, Hero hero, Spline spline)
    {
        _data = data;
        _health = _data.Health;
        _damage = _data.Damage;
        _speed = _data.Speed;
        _reward = _data.Reward;

        _hero = hero;

        _hasAttacked = false;

        stateMachine = new EnemyStateMachine(this, spline, _hero);
        stateMachine.Enter<EnemyMoveState>();
    }

    private void Update()
    {
        if (_pause)
            return;

        stateMachine.Update();

        if (!_hasAttacked && Vector3.Distance(transform.position, _hero.transform.position) < AttackRange )
        {
            stateMachine.Enter<EnemyAttackState>();
            _hasAttacked = true;
        }
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        Debug.Log($"Enemy ApplyDamage - {damage}");
        if (_health <= 0)
        {
            stateMachine.Enter<EnemyDeathState>();
        }
    }

    public void Kill()
    {
        Died?.Invoke();
    }

    public void ReturnToPoll()
    {
        CompletedPath?.Invoke();
        _health = _data.Health;
    }

    public void Pause() => _pause = true;

    public void Resume() => _pause = false;
}

