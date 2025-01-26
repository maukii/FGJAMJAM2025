using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class FlashOnDamage : MonoBehaviour
{

    [SerializeField] SkinnedMeshRenderer[] meshRenderers;
    [SerializeField] Material damageFlashMaterial;
    [SerializeField] float flashDuration = 0.2f;

    Health health;
    Material[][] originalMaterials;
    Coroutine damageFlashRoutine;


    void Awake() => health = GetComponent<Health>();

    void Start()
    {
        originalMaterials = new Material[meshRenderers.Length][];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].materials;
        }
    }

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
        foreach (var renderer in meshRenderers)
        {
            Material[] flashMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < flashMaterials.Length; i++)
            {
                flashMaterials[i] = damageFlashMaterial;
            }
            renderer.materials = flashMaterials;
        }

        yield return new WaitForSeconds(flashDuration);
        
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].materials = originalMaterials[i];
        }

        damageFlashRoutine = null;
    }
}