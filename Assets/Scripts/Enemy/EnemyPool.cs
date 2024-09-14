using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private readonly Queue<Enemy> _pool;
    private readonly EnemyFactory _enemyFactory;
    private readonly int _maxCount;

    public EnemyPool(EnemyFactory enemyFactory, int maxCount, EnemyType enemyType)
    {
        _pool = new Queue<Enemy>();
        _enemyFactory = enemyFactory;
        _maxCount = maxCount;
        PopulatePool(enemyType);
    }

    public Enemy GetEnemy(EnemyType type)
    {
        if (_pool.Count > 0)
        {
            var enemy = _pool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }

        if (_pool.Count < _maxCount)
        {
            var newEnemy = _enemyFactory.CreateEnemy(type, Vector3.zero);
            return newEnemy;
        }

        return null;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
    }

    private void PopulatePool(EnemyType type)
    {
        for (int i = 0; i < _maxCount; i++)
        {
            var enemy = _enemyFactory.CreateEnemy(type, Vector3.zero);
            enemy.gameObject.SetActive(false);
            _pool.Enqueue(enemy);
        }
    }
}
