using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class FlashOnDamage : MonoBehaviour
{
    [SerializeField] Material damageFlashMaterial;
    [SerializeField] float flashDuration = 0.2f;

    MeshRenderer meshRenderer;
    Health health;
    Material originalMaterial;
    Coroutine damageFlashRoutine;


    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        health = GetComponent<Health>();
    }

    void Start() => originalMaterial = meshRenderer.material;

    void OnEnable() => health.OnTakeDamage += OnTakeDamage;

    void OnDisable() => health.OnTakeDamage -= OnTakeDamage;

    void OnTakeDamage()
    {
        if (damageFlashRoutine != null)
            StopCoroutine(damageFlashRoutine);

        damageFlashRoutine = StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        meshRenderer.material = damageFlashMaterial;
        yield return new WaitForSeconds(flashDuration);
        meshRenderer.material = originalMaterial;
    }
}