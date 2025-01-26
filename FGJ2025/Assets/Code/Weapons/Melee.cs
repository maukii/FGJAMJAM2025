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
    [SerializeField] ParticleSystem particles;

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
        
        if (rb != null)
            rb.linearVelocity = meleeHitPoint.forward * currentWeapon.projectileSpeed;

        nextHitTime = Time.time + currentWeapon.fireRate;


        AudioManager.Instance.PlayRandomSound(bubbleMeleeAudios, Random.Range(0.9f, 1.1f));

	if (particles != null)
		particles.Play();
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

        nextHitTime = Time.time + Mathf.Max(0, meleeHitCooldown - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.AttackRate));
    }

    void OnDrawGizmos()
    {
        if (meleeHitPoint == null) return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(meleeHitPoint.position, meleeHitSize);
    }
}
