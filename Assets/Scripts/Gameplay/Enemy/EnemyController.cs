using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private enum State { Idle, Patrol, Chase, Caught }

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float catchDistance;

    private PlayerHealthController playerHealth;
    private PlayerDamageFXController plDamageFXController;
    private NavMeshAgent meshAgent;
    private EnemyVision enemyVision;
    private State state;
    private int currentPatrolIndex;

    private void Awake()
    {
        enemyVision = GetComponent<EnemyVision>();
        meshAgent = GetComponent<NavMeshAgent>();
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

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        meshAgent.isStopped = false;
        meshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void UpdatePatrol()
    {
        if (!meshAgent.pathPending && meshAgent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();
    }

    private void UpdateChase()
    {
        meshAgent.SetDestination(playerPosition.position);
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
            GoToNextPatrolPoint();
            Debug.Log("Враг потерял игрока и возвращается патрулировать");
        }
    }

    private void CheckVision()
    {
        if (enemyVision.SeeTarget(playerPosition))
        {
            state = State.Chase;
            Debug.Log("Враг увидел игрока и начинает преследовать");
        }
    }

    private void CatchPlayer()
    {
        state = State.Caught;
        meshAgent.isStopped = true;
        playerHealth.TakeDamage(80);
        plDamageFXController.PlayDamageFlash();
        Debug.Log("Враг догнал игрока");
    }
}
