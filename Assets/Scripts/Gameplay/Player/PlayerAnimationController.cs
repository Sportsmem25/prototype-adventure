using UnityEngine;
using Zenject;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerIdleController idleController;
    [SerializeField] private PlayerJumpController jumpController;

    private PlayerInputMain playerInput;

    [Inject]
    public void Construct(PlayerInputMain input)
    {
        playerInput = input;
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        Vector2 move = playerInput.MoveDirection.Value;
        float speed;


        if (move.magnitude < 0.1f)
            speed = 0f;
        else if (movementController.IsRunning)
            speed = 1f;
        else
            speed = 0.5f;

        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        animator.SetBool("IsSiting", idleController.IsSiting);
        animator.SetBool("IsJumping", jumpController.IsJumping);
    }
}