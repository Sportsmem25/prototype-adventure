

public class EnemyPatrolState : IEnemyState
{
    private readonly EnemyController enemy;

    public EnemyPatrolState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.SetHumanState(HumanState.Walk);
        enemy.GoToNextPatrolPoint();
    }

    public void Update()
    {
        enemy.UpdatePatrol();
        if (enemy.CanSeePlayer())
        {
            enemy.SetState(new EnemyChaseState(enemy));
        }
    }

    public void Exit()
    {

    }
}