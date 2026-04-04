
public class EnemyChaseState : IEnemyState
{
    private EnemyController enemy;

    public EnemyChaseState(EnemyController enemy)
    {
        this.enemy = enemy; 
    }

    public void Enter()
    {
        enemy.SetHumanState(HumanState.Chase);
    }

    public void Update()
    {
        enemy.MoveToPlayer();
        if (!enemy.CanSeePlayer())
        {
            enemy.SetState(new EnemyPatrolState(enemy));
            return;
        }

        if (enemy.IsPlayerInCatchRange())
        {
            enemy.SetState(new EnemyCatchState(enemy));
        }
    }

    public void Exit()
    {
        enemy.SetHumanState(HumanState.Walk);
    }
}