using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StaminaPlayerUIController : MonoBehaviour
{
    [SerializeField] private PlayerStaminaController staminaController;
    [SerializeField] private Image staminaBar;

    private void Start()
    {
        staminaController.Stamina.Subscribe(UpdateStaminaBar).AddTo(this);
    }

    private void UpdateStaminaBar(float value)
    {
        float normalizedValue = value / staminaController.MaxStamina;
        staminaBar.fillAmount = Mathf.Clamp01(normalizedValue);
    }
}