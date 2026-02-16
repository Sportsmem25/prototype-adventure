using UnityEngine;
using FMODUnity;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private EventReference walkEvent;
    [SerializeField] private EventReference runEvent;

    [SerializeField] private EventReference smellEvent;
    [SerializeField] private EventReference pickupEvent;
    [SerializeField] private EventReference damageEvent;

    private bool isWasMoving;
    private bool isWasRunning;
    private PlayerMovementController movementController;
    private PlayerStateController stateController;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        stateController = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        HandleMovementSounds();
    }

    public void PlayPickUp()
    {
        RuntimeManager.PlayOneShot(pickupEvent, transform.position);
    }

    public void PlaySmell()
    {
        RuntimeManager.PlayOneShot(smellEvent, transform.position);
    }

    public void PlayDamage()
    {
        RuntimeManager.PlayOneShot(damageEvent, transform.position);
    }

    private void HandleMovementSounds()
    {
        if (movementController.IsMoving && !isWasMoving)
            RuntimeManager.PlayOneShot(walkEvent, transform.position);

        if (movementController.IsRunning && !isWasRunning)
            RuntimeManager.PlayOneShot(runEvent, transform.position);

        isWasMoving = movementController.IsMoving;
        isWasRunning = movementController.IsRunning;
    }
}