using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] Transform meleeHitPoint;
    [SerializeField] float meleeHitSize = 3f;
    [SerializeField] LayerMask targetedLayermask;
    [SerializeField] float meleeHitCooldown = 0.5f;
    [SerializeField] int baseDamage = 1;
    [SerializeField] Animator anim;

    public int Damage => baseDamage + (int)UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.Damage);
    public bool IsAttacking => isAttacking;

    bool isAttacking = false;
    float nextHitTime = 0;
    PlayerInputHandler inputHandler;


    void Awake() => inputHandler = GetComponent<PlayerInputHandler>();

    void Update()
    {
        if (inputHandler.BubbleMelee && Time.time >= nextHitTime)
            Hit();

        if (inputHandler.StabMelee && Time.time >= nextHitTime)
            Stab();
    }

    void Hit()
    {
        isAttacking = true;

        anim.SetTrigger("Hit");

        var hits = Physics.OverlapSphere(meleeHitPoint.position, meleeHitSize, targetedLayermask);
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null && !health.IsBubbled)
                health.TakeDamage(Damage);
        }

        nextHitTime = Time.time + Mathf.Max(0, meleeHitCooldown - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.AttackRate));
    }

    void Stab()
    {
        isAttacking = true;

        anim.SetTrigger("Stab");

        var hits = Physics.OverlapSphere(meleeHitPoint.position, meleeHitSize, targetedLayermask);
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null && health.IsBubbled)
            {
                // TODO::
                // Pop the bubble
            }
        }

        nextHitTime = Time.time + Mathf.Max(0, meleeHitCooldown - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.AttackRate));
    }

    public void OnAttackAnimationCompleted() => isAttacking = false;

    void OnDrawGizmos()
    {
        if (meleeHitPoint == null) return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(meleeHitPoint.position, meleeHitSize);
    }
}
