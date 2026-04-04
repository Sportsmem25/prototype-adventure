
public class EnemyCatchState : IEnemyState
{
    private EnemyController enemy;

    public EnemyCatchState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.SetHumanState(HumanState.Catch);
        enemy.StopAgent();
        enemy.CatchPlayerInternal();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        
    }
}