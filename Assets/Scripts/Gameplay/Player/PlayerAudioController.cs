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
    private EventInstance smellInstance;

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        stateController = GetComponent<PlayerStateController>();
        footstepsInstance = RuntimeManager.CreateInstance(footstepsEvent);
        smellInstance = RuntimeManager.CreateInstance(smellEvent);
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
        PLAYBACK_STATE state;
        smellInstance.getPlaybackState(out state);
        if (state != PLAYBACK_STATE.PLAYING)
            smellInstance.start();
    }

    public void StopSmell()
    {
        smellInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayDamage()
    {
        RuntimeManager.PlayOneShot(damageEvent, transform.position);
    }

    private void HandleMovementSounds()
    {
        if (movementController.IsMoving)
        {
            // Проверка состояния, чтобы не вызывать Start() постоянно
            PLAYBACK_STATE state;
            footstepsInstance.getPlaybackState(out state);
            if (state == PLAYBACK_STATE.STOPPED)
                footstepsInstance.start();

            // Обновление параметра
            float speedValue = movementController.IsRunning ? 1f : 0f;
            footstepsInstance.setParameterByName("Speed", speedValue);
        }
        else
        {
            footstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()
    {
        footstepsInstance.release();
        smellInstance.release();
    }
}