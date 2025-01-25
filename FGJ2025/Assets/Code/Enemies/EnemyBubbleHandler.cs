using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyBubbleHandler : MonoBehaviour
{
    [SerializeField] BubbleWobble bubblePrefab;

    Health health;


    void Awake() => health = GetComponent<Health>();

    void OnEnable() => health.OnDeath += OnEnemyDeath;

    void OnDisable() => health.OnDeath -= OnEnemyDeath;

    void OnEnemyDeath()
    {
        var bubbleInstance = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        transform.SetParent(bubbleInstance.transform);
    }
}
