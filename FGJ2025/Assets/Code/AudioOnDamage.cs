using UnityEngine;

[RequireComponent(typeof(Health), typeof(AudioSource))]
public class AudioOnDamage : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;

    Health health;


    void Awake() => health = GetComponent<Health>();

    void OnEnable() => health.OnTakeDamage += OnTakeDamage;

    void OnDisable() => health.OnTakeDamage -= OnTakeDamage;

    void OnTakeDamage()
    {
        var clip = clips[Random.Range(0, clips.Length)];
        source.clip = clip;
        source.Play();
    }
}
