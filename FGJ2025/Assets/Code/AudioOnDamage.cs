using UnityEngine;

[RequireComponent(typeof(Health))]
public class AudioOnDamage : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;

    Health health;


    void Awake() => health = GetComponent<Health>();

    void OnEnable() => health.OnTakeDamage += OnTakeDamage;

    void OnDisable() => health.OnTakeDamage -= OnTakeDamage;

    void OnTakeDamage()
    {
        AudioManager.Instance.PlayRandomSound(clips, Random.Range(0.9f, 1.1f));
    }
}
