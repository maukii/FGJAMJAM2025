using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] Transform meleeHitPoint;
    [SerializeField] float meleeHitSize = 3f;
    [SerializeField] LayerMask targetedLayermask; 
    [SerializeField] BubbleWobble bubblePrefab;

    float nextHitTime;
    PlayerInputHandler inputHandler;


    void Awake() => inputHandler = GetComponent<PlayerInputHandler>();

    void Update()
    {
        if (inputHandler.IsShooting && Time.time >= nextHitTime)
            Hit();
    }

    void Hit()
    {
        var hits = Physics.OverlapSphere(meleeHitPoint.position, meleeHitSize, targetedLayermask);
        foreach (var hit in hits)
        {
            if (hit.GetComponentInParent<BubbleWobble>() != null) continue;

            var bubble = Instantiate(bubblePrefab, hit.transform.position, hit.transform.rotation);
            hit.transform.SetParent(bubble.transform);
        }
    }

    void OnDrawGizmos()
    {
        if (meleeHitPoint == null) return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(meleeHitPoint.position, meleeHitSize);
    }
}
