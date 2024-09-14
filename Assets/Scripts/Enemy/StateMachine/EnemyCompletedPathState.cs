public class EnemyCompletedPathState : IEnemyState
{
    private Enemy enemy;

    public EnemyCompletedPathState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.gameObject.SetActive(false);
        enemy.ReturnToPoll();
    }

    public void Execute() { }

    public void Exit() { }
}

