using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    
    public event Action OnTakeDamage;
    public event Action OnDeath;
    
    int currentHealth;
    bool isDead = false;


    void Awake() => currentHealth = maxHealth;

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0 && !isDead)
            Die();
    }

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }
}
