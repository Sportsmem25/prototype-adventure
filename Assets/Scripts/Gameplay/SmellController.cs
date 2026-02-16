using UnityEngine;
using UniRx;

public class SmellController : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> IsSmelling => isSmelling;
    public IReadOnlyReactiveProperty<bool> IsOnCooldown => isOnCooldown;
    public IReadOnlyReactiveProperty<float> SmellAmount => smellAmount;

    public float MaxSmellTime => maxSmellTime;

    [Header("Settings")]
    [SerializeField] private PlayerAudioController playerAudioController;
    [SerializeField] private float maxSmellTime;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float smellConsumeRate;
    [SerializeField] private float smellRecoverRate;

    private ReactiveProperty<bool> isSmelling;
    private ReactiveProperty<bool> isOnCooldown;
    private ReactiveProperty<float> smellAmount;
    private KeyCode smellKey = KeyCode.V;

    private void Awake()
    {
        isSmelling = new ReactiveProperty<bool>(false);
        isOnCooldown = new ReactiveProperty<bool>(false);
        smellAmount = new ReactiveProperty<float>(maxSmellTime);
    }

    private void Update()
    {
        if (isOnCooldown.Value)
            return;

        bool isWantSmell = Input.GetKey(smellKey) && smellAmount.Value > 0f;
        isSmelling.Value = isWantSmell;

        if (isSmelling.Value)
        {
            ConsumeSmell();
        }
        else
        {
            RecoverSmell();
        }
    }

    private void ConsumeSmell()
    {
        smellAmount.Value -= smellConsumeRate * Time.deltaTime;
        //playerAudioController.PlaySmell();
        if (smellAmount.Value <= 0)
        {
            smellAmount.Value = 0;
            ReloadSmell();
        }
    }

    private void RecoverSmell()
    {
        if (smellAmount.Value >= maxSmellTime)
            return;

        smellAmount.Value += smellConsumeRate * Time.deltaTime;
        smellAmount.Value = Mathf.Min(smellAmount.Value, maxSmellTime);
    }

    private void ReloadSmell()
    {
        isSmelling.Value = false;
        isOnCooldown.Value = true;
        Observable.Timer(System.TimeSpan.FromSeconds(cooldownTime)).Subscribe(_ =>
        {
            smellAmount.Value = maxSmellTime;
            isOnCooldown.Value = false;
        }).AddTo(this);
    }
}
