using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public float MaxHealth => maxHealth;
    public float CurretnHealth => currentHealth;
    public bool IsLowHealth => currentHealth <= maxHealth * 0.3f;

    [SerializeField] private float maxHealth;
    [SerializeField] private float recoverDelay;
    [SerializeField] private float recoverSpeed;
    [SerializeField] private PlayerAudioController audioController;

    private float currentHealth;
    private float lastDamageTime;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        TryRecover();
    }

    public void TakeDamage(float amount)
    {
        audioController.PlayDamage();
        currentHealth = Mathf.Max(0f, currentHealth - amount);
        lastDamageTime = Time.time;
    }


    private void TryRecover()
    {
        if (currentHealth >= maxHealth)
            return;

        if (Time.time - lastDamageTime < recoverDelay)
            return;
        
        currentHealth += recoverSpeed * Time.deltaTime;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }
}