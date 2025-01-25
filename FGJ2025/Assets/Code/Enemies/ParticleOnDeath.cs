using UnityEngine;

public class ParticleOnDeath : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    Health health;


    void Awake() => health = GetComponent<Health>();

    void OnEnable() => health.OnDeath += OnDeath;

    void OnDisable() => health.OnDeath -= OnDeath;

    void OnDeath() => Instantiate(particle, health.BubbleCenterPos.position, Quaternion.identity);
}
