using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private EventReference footstepsEvent;

    [SerializeField] private EventReference smellEvent;
    [SerializeField] private EventReference pickupEvent;
    [SerializeField] private EventReference damageEvent;

    private PlayerMovementController movementController;
    private PlayerStateController stateController;
    private EventInstance footstepsInstance;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        stateController = GetComponent<PlayerStateController>();
        footstepsInstance = RuntimeManager.CreateInstance(footstepsEvent);
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
        if (movementController.IsMoving)
        {
            PLAYBACK_STATE state;
            footstepsInstance.getPlaybackState(out state);
            if (state != PLAYBACK_STATE.PLAYING)
                footstepsInstance.start();

            if (movementController.IsRunning)
                footstepsInstance.setParameterByName("Speed", 1);
            else
                footstepsInstance.setParameterByName("Speed", 0);

            RuntimeManager.AttachInstanceToGameObject(footstepsInstance, transform);
        }
        else
        {
            footstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}