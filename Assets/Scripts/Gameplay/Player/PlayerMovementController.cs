using System;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed;

    public bool IsRunning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsRotating { get; private set; }

    private CharacterController characterController;
    private PlayerStaminaController playerStaminaController;
    private PlayerIdleController idleController;
    private PlayerJumpController jumpController;
    private PlayerInputMain playerInput;
    private Vector3 velocity;
    private PlayerStateController stateController;
    [SerializeField] private Camera camera;

    [Inject]
    public void Construct(PlayerInputMain input)
    {
        playerInput = input;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStaminaController = GetComponent<PlayerStaminaController>();
        idleController = GetComponent<PlayerIdleController>();
        jumpController = GetComponent<PlayerJumpController>();
        stateController = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        Vector2 input = playerInput.MoveDirection.Value;

        bool hasForwardInput = Mathf.Abs(input.y) > 0.01f;
        bool hasRotateInput = Mathf.Abs(input.x) > 0.01f;
        Debug.Log($"hasRotateInput - {hasRotateInput}");
        IsMoving = hasForwardInput;
        IsRotating = hasRotateInput;

        if (IsRotating && !jumpController.IsJumping)
        {
            CharacterRotation(input.x);
        }

        Vector3 move = Vector3.zero;

        if (hasForwardInput)
        {
            Vector3 forward = transform.forward;
            move = forward * input.y;
        }

        bool isWantsRun = playerInput.Sprint.Value;
        IsRunning = isWantsRun && playerStaminaController.CanRun && IsMoving;
        playerStaminaController.Tick(IsRunning);
        float speed = IsRunning ? runSpeed : walkSpeed;
        Vector3 horizontalMove = move.normalized * speed;

        float verticalVelocity = jumpController.Tick(Input.GetKeyDown(KeyCode.Space));
        
        Vector3 finalMove = horizontalMove;
        finalMove.y = verticalVelocity;

        characterController.Move(finalMove * Time.deltaTime);
        idleController.Tick(IsMoving, IsRotating);
        UpdateState();
    }

    private void CharacterRotation(float direction)
    {
        float rotation = direction * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);
    }

    private void UpdateState()
    {
        if (jumpController.IsJumping)
        {
            stateController.SetState(PlayerState.Jump);
            return;
        }  

        if (IsMoving)
        {
            stateController.SetState(PlayerState.Walk);
            return;
        } 

        if (IsRunning)
        {
            stateController.SetState(PlayerState.Walk);
            return;
        }

        if (idleController.IsSiting)
        {
            stateController.SetState(PlayerState.Sit);
            return;
        } 

        stateController.SetState(PlayerState.Idle);
    }
}