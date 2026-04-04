using UnityEngine;
using UnityEngine.UI;

public class DamagePlayerUI : MonoBehaviour
{
    [SerializeField] private Image imageDamage;
    [SerializeField] private float damageFlashAlpha;
    [SerializeField] private float lowHealthAlpha;

    private PlayerHealthController playerHealth;

    private float currentAlpha;
    private float fadeSpeed = 2f;

    private void Update()
    {
        UpdateFlash();
        ApplyAlpha();
    }

    public void PlayDamageFlash()
    {
        currentAlpha = damageFlashAlpha;
    }

    private void UpdateFlash()
    {
        if(currentAlpha > 0f)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, 0f, fadeSpeed * Time.deltaTime);
        }
    }
    private void ApplyAlpha()
    {
        float lowHealthOverlay = playerHealth.IsLowHealth ? lowHealthAlpha : 0f;
        float finalAlpha = Mathf.Max(currentAlpha, lowHealthOverlay);
        var c = imageDamage.color;
        c.a = finalAlpha;
        imageDamage.color = c;
    }
}