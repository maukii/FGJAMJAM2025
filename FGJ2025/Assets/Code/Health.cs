using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    
    public bool IsBubbled => isDead;
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

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            return;
        }
        
        OnTakeDamage?.Invoke();
    }

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }
}
