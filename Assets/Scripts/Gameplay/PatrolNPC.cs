using UnityEngine;

public class PatrolNPC : HumanBase
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime;

    private int currentIndexPatrolPoints;
    private float waitTimer;

    private void Start()
    {
        MoveToNextPatrolPoint();
        Debug.Log(agent.isOnNavMesh);
    }

    private void Update()
    {
        UpdateAnimator();
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= waitTime)
            {
                MoveToNextPatrolPoint();
                waitTimer = 0;
            }
        }
    }

    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentIndexPatrolPoints].position);
        SetState(HumanState.Walk);
        currentIndexPatrolPoints = (currentIndexPatrolPoints + 1) % patrolPoints.Length;
    }
}