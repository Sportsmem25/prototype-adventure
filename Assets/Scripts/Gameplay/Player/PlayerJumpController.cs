using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJumpController : MonoBehaviour
{
    public bool IsJumping {  get; private set; }
    
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public float Tick(bool isJumpInput)
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f;

            if (isJumpInput)
            {
                verticalVelocity = jumpForce;
                IsJumping = true;
                return verticalVelocity;
            }
            IsJumping = false;
        }
        verticalVelocity += gravity * Time.deltaTime;
        return verticalVelocity;
    }
}