using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyController : HumanBase
{
    public Transform PlayerTransform => playerPosition;
    public EnemyVision Vision => enemyVision;
    public float CatchDistance => catchDistance;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float catchDistance;
    [SerializeField] private float waitTime;

    private IDamageable playerHealth;
    private IEnemyState currentState;
    private IDamageFX damageFX;
    private EnemyVision enemyVision;
    private int currentPatrolIndex;
    private float waitTimer;
    private bool isWaiting;

    [Inject]
    public void Construct(IDamageable playerHealth, IDamageFX damageFX)
    {
        this.playerHealth = playerHealth;
        this.damageFX = damageFX;
    }

    protected override void Awake()
    {
        base.Awake();
        enemyVision = GetComponent<EnemyVision>();
    }

    private void Start()
    {
        SetState(new EnemyPatrolState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void SetState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void SetHumanState(HumanState state)
    {
        SetState(state);
    }

    public void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerPosition.position);
    }

    public bool CanSeePlayer()
    {
        return enemyVision.SeeTarget(playerPosition);
    }

    public bool IsPlayerInCatchRange()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        return distance <= catchDistance;
    }

    protected override void UpdateStateFromMovement()
    {

    }

    public void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        SetState(HumanState.Walk);
    }

    public void UpdatePatrol()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                isWaiting = false;
                GoToNextPatrolPoint();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StartWaiting();
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    public void CatchPlayerInternal()
    {
        StopAgent();
        SetState(HumanState.Catch);
        playerHealth.TakeDamage(80);
        damageFX.PlayDamageFlash();
        Debug.Log("Враг догнал игрока");
    }

    private void StartWaiting()
    {
        isWaiting = true;
        waitTimer = waitTime;
        agent.isStopped = true;
        SetState(HumanState.Idle);
    }
}