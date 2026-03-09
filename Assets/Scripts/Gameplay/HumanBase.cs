using UnityEngine;
using UnityEngine.AI;

public abstract class HumanBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    protected HumanState currentState;

    protected virtual void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        UpdateStateFromMovement();
        UpdateAnimator();
    }

    protected virtual void UpdateStateFromMovement()
    {
        float speed = agent.velocity.magnitude;
        if (speed > 0.1f)
            SetState(HumanState.Walk);
        else
            SetState(HumanState.Idle);
    }

    protected virtual void UpdateAnimator()
    {        
        float speed = (agent !=null && agent.enabled) ? agent.velocity.magnitude : 0;

        float animatorSpeed = (currentState == HumanState.Chase) ? 0 : speed;

        animator.SetFloat("Speed", animatorSpeed);
        animator.SetBool("IsSitting", currentState == HumanState.Sit);
        animator.SetBool("IsChasing", currentState == HumanState.Chase);
        if (currentState == HumanState.Catch)
            animator.SetTrigger("Catch");
    }

    protected void SetState(HumanState state)
    {
        if (currentState == state)
            return;
        currentState = state;
    }
}