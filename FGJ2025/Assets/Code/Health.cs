using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject BubblePrefab;
    private GameObject bubbleGO;

    [SerializeField] int maxHealth = 100;
    
    public bool IsBubbled => isBubbled;
    public event Action OnTakeDamage;
    public event Action OnBubbled;
    public event Action OnDeath;
    
    int currentHealth;
    bool isDead = false;
    bool isBubbled = false;


    void Start() => SetFullHealth();

    public void SetFullHealth() => currentHealth = maxHealth;

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0 && !isDead)
        {
            if (!isBubbled)
            {
                Bubble();
                return;
            }
            else
            {
                Die();
                return;
            }
        }
        
        OnTakeDamage?.Invoke();
    }

    void Bubble()
    {
        isBubbled = true;
        bubbleGO = Instantiate(BubblePrefab, transform);
        OnBubbled?.Invoke();
    }

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }
}
