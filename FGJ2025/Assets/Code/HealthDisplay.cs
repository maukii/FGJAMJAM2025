using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Image healthGraphic;
    [SerializeField] float animationDuration = 0.5f;
    [SerializeField] float punchRotationStrength = 1.25f;
    [SerializeField] float punchScaleStrength = 1.25f;
    [SerializeField] float punchDuration = 0.35f;


    void OnEnable() => playerHealth.OnHealthChanged += OnHealthChanged;

    void OnDisable() => playerHealth.OnHealthChanged -= OnHealthChanged;

    void OnHealthChanged(int currentHealth, int maxHealth)
    {
        healthGraphic.DOKill();
        healthGraphic.transform.DOKill();

        float healthPercentage = (float)currentHealth / maxHealth;
        healthGraphic.DOFillAmount(healthPercentage, animationDuration).SetEase(Ease.OutBounce);

        if (currentHealth < maxHealth)
        {
            healthGraphic.transform.DOPunchScale(Vector3.one * punchScaleStrength, punchDuration);
            healthGraphic.transform.DOPunchRotation(Vector3.forward * punchRotationStrength, punchDuration);
        }
    }
}
