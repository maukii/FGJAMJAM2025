using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    Health health;


    void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        health.OnTakeDamage += OnTakeDamage;
        health.OnDeath += OnDeath;
    }

    void OnDisable()
    {
        health.OnTakeDamage -= OnTakeDamage;
        health.OnDeath -= OnDeath;
    }

    
    void OnTakeDamage()
    {
        animator.SetTrigger("TakeDamage");
    }

    void OnDeath()
    {
        animator.SetTrigger("Die");
    }
}
