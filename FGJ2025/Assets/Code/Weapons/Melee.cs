using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] AudioClip[] bubbleMeleeAudios;
    [SerializeField] AudioClip[] bubblePopAudios;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] Transform meleeHitPoint;
    [SerializeField] float meleeHitSize = 3f;
    [SerializeField] LayerMask targetedLayermask;
    [SerializeField] float meleeHitCooldown = 0.5f;
    [SerializeField] int baseDamage = 1;
    [SerializeField] Animator anim;

    public int Damage => baseDamage + (int)UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.Damage);

    float nextHitTime = 0;
    PlayerInputHandler inputHandler;


    void Awake() => inputHandler = GetComponent<PlayerInputHandler>();

    void Update()
    {
        if (GameStateManager.Instance.CurrentGameState != GameState.Playing) return;

        if (inputHandler.BubbleMelee && Time.time >= nextHitTime)
            Hit();

        if (inputHandler.StabMelee && Time.time >= nextHitTime)
            Stab();
    }

    void Hit()
    {
        anim.SetTrigger("Hit");

        Projectile projectileInstance = Instantiate(currentWeapon.projectilePrefab, meleeHitPoint.position, meleeHitPoint.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        var baseDamping = rb.linearDamping;
        
        if (rb != null)
        {
            rb.linearVelocity = meleeHitPoint.forward * (currentWeapon.projectileSpeed + UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileSpeed));
            rb.linearDamping = Mathf.Max(0, baseDamping - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileRange));
        }

        if (UpgradesHandler.Instance.HasUpgrade(UpgradeType.ProjectileCount))
        {
            Quaternion leftRotation = Quaternion.Euler(0, -45, 0); // Rotate -45 degrees around Y-axis
            Vector3 leftDirection = leftRotation * meleeHitPoint.forward;

            // Instantiate left projectile
            Projectile leftProjectile = Instantiate(currentWeapon.projectilePrefab, meleeHitPoint.position, Quaternion.LookRotation(leftDirection));
            Rigidbody leftRb = leftProjectile.GetComponent<Rigidbody>();
            if (leftRb != null)
            {
                leftRb.linearVelocity = leftDirection * (currentWeapon.projectileSpeed + UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileSpeed));
                leftRb.linearDamping = Mathf.Max(0, baseDamping - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileRange));
            }

            // Calculate 45-degree rotation to the right
            Quaternion rightRotation = Quaternion.Euler(0, 45, 0); // Rotate +45 degrees around Y-axis
            Vector3 rightDirection = rightRotation * meleeHitPoint.forward;

            // Instantiate right projectile
            Projectile rightProjectile = Instantiate(currentWeapon.projectilePrefab, meleeHitPoint.position, Quaternion.LookRotation(rightDirection));
            Rigidbody rightRb = rightProjectile.GetComponent<Rigidbody>();
            if (rightRb != null)
            {
                rightRb.linearVelocity = rightDirection * (currentWeapon.projectileSpeed + UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileSpeed));
                rightRb.linearDamping = Mathf.Max(0, baseDamping - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileRange));
            }
        }

        var baseFireRate = currentWeapon.fireRate;
        nextHitTime = Time.time + Mathf.Max(0.5f, baseFireRate - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.AttackRate));
        AudioManager.Instance.PlayRandomSound(bubbleMeleeAudios, Random.Range(0.9f, 1.1f));
    }

    void Stab()
    {
        anim.SetTrigger("Stab");

        var hits = Physics.OverlapSphere(meleeHitPoint.position, meleeHitSize, targetedLayermask);
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Health>();
            if (health != null && health.IsBubbled)
            {
                health.TakeDamage(1);
                AudioManager.Instance.PlayRandomSound(bubblePopAudios, Random.Range(0.9f, 1.1f));
                CameraShaker.Instance.Shake(0.2f, 0.2f, 10, 1, DG.Tweening.Ease.InOutFlash);
            }
        }

        var baseFireRate = currentWeapon.fireRate;
        nextHitTime = Time.time + Mathf.Max(0.5f, baseFireRate - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.AttackRate));
    }

    void OnDrawGizmos()
    {
        if (meleeHitPoint == null) return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(meleeHitPoint.position, meleeHitSize);
    }
}
