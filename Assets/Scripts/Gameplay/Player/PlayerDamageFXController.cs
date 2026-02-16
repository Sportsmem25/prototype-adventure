using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageFXController : MonoBehaviour
{
    [SerializeField] private PlayerHealthController health;
    [SerializeField] private Image damageImage;
    [SerializeField] private float flashAlpha;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float lowHealthAlpha;

    private float currentAlpha;

    private void Update()
    {
        ScreenLowHealth();
        Fade();
    }

    public void PlayDamageFlash()
    {
        currentAlpha = flashAlpha;
        SetAlpha(currentAlpha);
    }

    private void ScreenLowHealth()
    {
        if (health.IsLowHealth)
        {
            currentAlpha = Mathf.Max(currentAlpha, lowHealthAlpha);
        }
    }

    private void Fade()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, 0f, flashAlpha * Time.deltaTime);
        SetAlpha(currentAlpha);
    }

    private void SetAlpha(float alpha)
    {
        var c = damageImage.color;
        c.a = alpha;
        damageImage.color = c;
    }
}