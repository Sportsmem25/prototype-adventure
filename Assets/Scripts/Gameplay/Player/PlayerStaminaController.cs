using UnityEngine;
using UniRx;

public class PlayerStaminaController : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> Stamina => stamina;
    public bool CanRun => !_isExhausted && stamina.Value > 0f;
    public float MaxStamina => maxStamina;

    [SerializeField] private float maxStamina;
    [SerializeField] private float recoverSpeed;
    [SerializeField] private float exhaustionCooldown;

    public bool _isExhausted;
    private ReactiveProperty<float> stamina;

    private void Awake()
    {
        stamina = new ReactiveProperty<float>(maxStamina);
        _isExhausted = false;
    }

    /// <summary>
    /// Фактический бег с учетом CanRun
    /// </summary>
    /// <param name="isRunning"></param>
    public void Tick(bool isRunning)
    {
        if (isRunning)
            Consume();
        else 
            Recover();
    }

    public void RestoreStamina(float amount)
    {
        stamina.Value = Mathf.Clamp(stamina.Value + amount, 0f, maxStamina);
    }

    private void Consume()
    {
        if (_isExhausted)
            return;

        stamina.Value -= Time.deltaTime;
        if (stamina.Value <= 0)
        {
            stamina.Value = 0;
            StartExhaustion();
        }
    }

    private void StartExhaustion()
    {
        _isExhausted = true;
        Observable.Timer(System.TimeSpan.FromSeconds(exhaustionCooldown)).Subscribe(_ =>
        {
            _isExhausted = false;
        }).AddTo(this);

    }

    private void Recover()
    {
        if (_isExhausted)
            return;

        if (stamina.Value >= maxStamina)
            return;

        stamina.Value += recoverSpeed * Time.deltaTime;
        stamina.Value = Mathf.Min(stamina.Value, maxStamina);
    }
}