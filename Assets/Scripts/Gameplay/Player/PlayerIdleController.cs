using UnityEngine;

public class PlayerIdleController : MonoBehaviour
{
    public bool IsSiting => isSitting;

    [SerializeField] private float timeToSit;

    private float idleTimer;
    private bool isSitting;

    private PlayerJumpController jumpController;

    private void Awake()
    {
        jumpController = GetComponent<PlayerJumpController>();
    }

    public void Tick(bool isMoving, bool isRotating)
    {
        if ((isMoving || isRotating))
        {
            idleTimer = 0;
            isSitting = false;
            return;
        }
        idleTimer += Time.deltaTime;

        if (idleTimer >= timeToSit)
            isSitting = true;
    }

    public void ResetIdle()
    {
        idleTimer = 0;
        isSitting = false;
    }

    // ÓÄÀËÈÒÜ?!
    public void Siting(bool isMoving)
    {
        if (isMoving)
        {
            idleTimer = 0;
            isSitting = false;
            return;
        }
        idleTimer += Time.deltaTime;
        if (idleTimer >= timeToSit)
        {
            isSitting = true;
        }
    }
}