using UnityEngine;
using UnityEngine.AI;

public class EnemyController : HumanBase
{
    private enum State { Idle, Patrol, Chase, Caught }

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float catchDistance;
    [SerializeField] private float waitTime;

    private PlayerHealthController playerHealth;
    private PlayerDamageFXController plDamageFXController;
    private EnemyVision enemyVision;
    private State state;
    private int currentPatrolIndex;
    private float waitTimer;
    private bool isWaiting;

    protected override void Awake()
    {
        base.Awake();

        enemyVision = GetComponent<EnemyVision>();
        playerHealth = FindObjectOfType<PlayerHealthController>();
        plDamageFXController = FindObjectOfType<PlayerDamageFXController>();
    }

    private void Start()
    {
        state = State.Patrol;
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Patrol:
                UpdatePatrol();
                CheckVision();
                break;

            case State.Chase: 
                UpdateChase();
                CheckCatchPlayer();
                CheckVisionLost();
                break;

            case State.Caught:

                break;
        }

    }

    protected override void UpdateStateFromMovement()
    {
        if (state == State.Chase || state == State.Caught) 
            return;

        base.UpdateStateFromMovement();
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        SetState(HumanState.Walk);
    }

    private void UpdatePatrol()
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

    private void UpdateChase()
    {
        agent.isStopped = false;
        agent.SetDestination(playerPosition.position);
    }

    private void CheckCatchPlayer()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        if(distance <= catchDistance)
            CatchPlayer();
    }

    private void CheckVisionLost()
    {
        if (!enemyVision.SeeTarget(playerPosition))
        {
            state = State.Patrol;
            SetState(HumanState.Walk);
            GoToNextPatrolPoint();
            Debug.Log("Враг потерял игрока и возвращается патрулировать");
        }
    }

    private void CheckVision()
    {
        if (enemyVision.SeeTarget(playerPosition))
        {
            state = State.Chase;
            SetState(HumanState.Chase);
            Debug.Log("Враг увидел игрока и начинает преследовать");
        }
    }

    private void CatchPlayer()
    {
        state = State.Caught;
        agent.isStopped = true;
        SetState(HumanState.Catch);
        playerHealth.TakeDamage(80);
        plDamageFXController.PlayDamageFlash();
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
