using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject BubblePrefab;
    public Transform BubbleCenterPos;
    private GameObject bubbleGO;

    [SerializeField] bool canBubble = true;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float unbubbleDuration = 5.0f;
    
    public bool IsBubbled => isBubbled;
    public event Action<int, int> OnHealthChanged;
    public event Action OnTakeDamage;
    public event Action OnBubbled;
    public event Action OnDeath;
    
    int currentHealth;
    bool isDead = false;
    bool isBubbled = false;


    void Start() => SetFullHealth();

    public void SetFullHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        OnTakeDamage?.Invoke();
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            if (!isBubbled && canBubble)
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
    }

    void Bubble()
    {
        isBubbled = true;
        bubbleGO = Instantiate(BubblePrefab, transform);
	bubbleGO.GetComponent<BubbleWobble>().OnDestroyed += Unbubble;

	Destroy(bubbleGO, unbubbleDuration);

        if(BubbleCenterPos)
        {
            bubbleGO.transform.position = BubbleCenterPos.position;
        }

        OnBubbled?.Invoke();
    }

    void Unbubble()
    {
	if (!isBubbled)
		return;

        isBubbled = false;
	SetFullHealth();
    }

    void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
    }
}
