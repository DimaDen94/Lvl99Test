using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour, IPausable
{
    protected ActiveEnemyHolder _activeEnemyHolder;

    protected float _attackRange;
    protected float _attackInterval;
    protected float _damage;

    private float _nextAttackTime;
    private bool _pause;

    public void Construct(ActiveEnemyHolder activeEnemyHolder, StructureData structureData)
    {
        _activeEnemyHolder = activeEnemyHolder;
        _attackRange = structureData.AttackRange;
        _attackInterval = structureData.AttackInterval;
        _damage = structureData.Damage;
        _nextAttackTime = Time.time;
    }

    private void Update()
    {
        if (_pause)
            return;

        if (Time.time >= _nextAttackTime)
        {
            AttackNearestEnemy();
            _nextAttackTime = Time.time + _attackInterval;
        }
    }

    protected void AttackNearestEnemy()
    {
        List<Enemy> enemies = _activeEnemyHolder.GetActiveEnemies();
        Enemy nearestEnemy = null;
        float shortestDistance = _attackRange;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            DealDamage(nearestEnemy);
        }
    }

    protected abstract void DealDamage(Enemy enemy);

    public void Pause() => _pause = true;

    public void Resume() => _pause = false;
}
