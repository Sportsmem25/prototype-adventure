using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SmellUIController : MonoBehaviour
{
    [SerializeField] private SmellController smellController;
    [SerializeField] private Image smellBar;

    private float pulseThreshold = 0.3f;
    private float pulseSpeed = 4f;
    private bool isPulsing;

    private void Start()
    {
        smellController.SmellAmount.Subscribe(UpdateBar).AddTo(this);
    }

    private void Update()
    {
        if(isPulsing)
        {
            float alpha = 0.5f + Mathf.Sin(pulseSpeed * Time.time) * 0.5f;
            smellBar.color = new Color(1f, 0.6f, 0.6f, alpha);
        }
    }

    private void UpdateBar(float value)
    {
        float normalizedValue = value / smellController.MaxSmellTime;
        smellBar.fillAmount = Mathf.Clamp01(normalizedValue);

        isPulsing = normalizedValue <= pulseThreshold;
        if(!isPulsing)
            smellBar.color = Color.white;
    }
}