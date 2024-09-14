public class EnemyAttackState : IEnemyState
{
    private Enemy enemy;
    private Hero hero;

    public EnemyAttackState(Enemy enemy, Hero hero)
    {
        this.enemy = enemy;
        this.hero = hero;
    }

    public void Enter()
    {
        hero.ApplyDamage(enemy.Damage);
        enemy.StateMachine.Enter<EnemyMoveState>();
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
    }
}
