using UnityEngine;

public class BonePickUp : MonoBehaviour
{
    [SerializeField] private float staminaRestore;

    [SerializeField] private PlayerAudioController playerAudioController;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStaminaController stamina = other.GetComponent<PlayerStaminaController>();
        stamina.RestoreStamina(staminaRestore);
        playerAudioController.PlayPickUp();
        Destroy(gameObject);
    }
}