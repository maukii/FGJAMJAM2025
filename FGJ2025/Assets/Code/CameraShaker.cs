using UnityEngine;
using DG.Tweening;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;


    void Awake() => instance = this;

    public void Shake(float duration, float force = 0.1f, int vibrato = 10, int elasticity = 1, Ease easing = Ease.Unset)
    {
        var direction = Random.insideUnitCircle.normalized * force;
        transform.DOPunchPosition(direction, duration, vibrato, elasticity).SetEase(easing);
    }
}