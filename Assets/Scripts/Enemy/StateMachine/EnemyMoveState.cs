using UnityEngine;
using UnityEngine.Splines;

public class EnemyMoveState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly Spline _spline;
    private float _splineT = 0f;

    public EnemyMoveState(Enemy enemy, Spline spline)
    {
        _enemy = enemy;
        _spline = spline;
    }

    public void Enter() { }

    public void Execute()
    {
        _splineT += Time.deltaTime * (_enemy.Speed / 100);

        if (_splineT > 1f)
        {
            _enemy.StateMachine.Enter<EnemyCompletedPathState>();
            return;
        }

        Vector3 splinePosition = SplineUtility.EvaluatePosition(_spline, _splineT);
        splinePosition.z = _enemy.transform.position.z;
        _enemy.transform.position = splinePosition;
    }

    public void Exit() { }
}
