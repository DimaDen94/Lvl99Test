public class EnemyDeathState : IEnemyState
{
    private Enemy enemy;

    public EnemyDeathState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.gameObject.SetActive(false);
        enemy.Kill();
    }

    public void Execute() { }

    public void Exit() { }
}

