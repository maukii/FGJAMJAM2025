using UnityEngine;
using DG.Tweening;

public class CameraShakeOnDamage : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.2f;
    [SerializeField] float shakeForce = 0.2f;
    [SerializeField] int vibrato = 10;
    [SerializeField] int elesticity = 1;
    [SerializeField] Ease easing = Ease.Unset;

    Health health;


    void Awake() => health = GetComponent<Health>();

    void OnEnable() => health.OnTakeDamage += OnTakeDamage;

    void OnDisable() => health.OnTakeDamage -= OnTakeDamage;

    void OnTakeDamage() => CameraShaker.Instance.Shake(shakeDuration, shakeForce, vibrato, elesticity, easing);
}
