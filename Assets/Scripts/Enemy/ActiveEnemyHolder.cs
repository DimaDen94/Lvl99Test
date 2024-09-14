using System.Collections.Generic;
using System.Linq;

public class ActiveEnemyHolder: IPausable
{
    private readonly HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();

    public void Add(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }

    public List<Enemy> GetActiveEnemies()
    {
        return _activeEnemies.Where(e => e.gameObject.activeSelf).ToList();
    }


    public void Pause()
    {
        foreach (var enemy in _activeEnemies)
        {
            enemy.Pause();
        }
    }

    public void Resume()
    {
        foreach (var enemy in _activeEnemies)
        {
            enemy.Resume();
        }
    }
}
