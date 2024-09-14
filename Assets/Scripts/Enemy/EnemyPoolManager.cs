using System.Collections.Generic;

public class EnemyPoolManager
{
    private readonly Dictionary<EnemyType, EnemyPool> _enemyPools = new Dictionary<EnemyType, EnemyPool>();
    private readonly ActiveEnemyHolder _activeEnemyManager;

    public event System.Action<int> EnemyDied;

    public EnemyPoolManager(EnemyFactory enemyFactory, Dictionary<EnemyType, int> enemyCounts, ActiveEnemyHolder activeEnemyManager)
    {
        _activeEnemyManager = activeEnemyManager;
        InitializePools(enemyFactory, enemyCounts);
    }

    public Enemy GetEnemy(EnemyType type)
    {
        if (_enemyPools.TryGetValue(type, out var pool))
        {
            var enemy = pool.GetEnemy(type);
            if (enemy != null)
            {
                _activeEnemyManager.Add(enemy);
                SubscribeToEnemyEvents(enemy, type);
            }
            return enemy;
        }
        return null;
    }

    private void InitializePools(EnemyFactory enemyFactory, Dictionary<EnemyType, int> enemyCounts)
    {
        foreach (var kvp in enemyCounts)
        {
            var pool = new EnemyPool(enemyFactory, kvp.Value, kvp.Key);
            _enemyPools[kvp.Key] = pool;
        }
    }

    private void ReturnToPool(Enemy enemy, EnemyType type)
    {
        if (_enemyPools.TryGetValue(type, out var pool))
        {
            UnsubscribeFromEnemyEvents(enemy, type);
            pool.ReturnEnemy(enemy);
            _activeEnemyManager.Remove(enemy);
        }
    }

    private void SubscribeToEnemyEvents(Enemy enemy, EnemyType type)
    {
        enemy.Died += () => OnEnemyDied(enemy, type);
        enemy.CompletedPath += () => OnEnemyCompletedPath(enemy, type);
    }

    private void UnsubscribeFromEnemyEvents(Enemy enemy, EnemyType type)
    {
        enemy.Died -= () => OnEnemyDied(enemy, type);
        enemy.CompletedPath -= () => OnEnemyCompletedPath(enemy, type);
    }

    private void OnEnemyDied(Enemy enemy, EnemyType type)
    {
        EnemyDied?.Invoke(enemy.Reward);
        ReturnToPool(enemy, type);
    }

    private void OnEnemyCompletedPath(Enemy enemy, EnemyType type)
    {
        ReturnToPool(enemy, type);
    }
}
