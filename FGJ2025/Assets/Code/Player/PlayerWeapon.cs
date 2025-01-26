using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] Weapon currentWeapon;
    [SerializeField] Transform bulletSpawnPoint;

    float nextFireTime = 0f;
    PlayerInputHandler inputHandler;


    void Awake() => inputHandler = GetComponent<PlayerInputHandler>();

    void Update() => HandleShooting();

    void HandleShooting()
    {
        if (inputHandler.BubbleMelee && Time.time >= nextFireTime)
            Shoot();
    }

    void Shoot()
    {
        Projectile projectileInstance = Instantiate(currentWeapon.projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            var baseDamping = rb.linearDamping;
            rb.linearVelocity = bulletSpawnPoint.forward * (currentWeapon.projectileSpeed + UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileSpeed));
            rb.linearDamping = Mathf.Max(0, baseDamping - UpgradesHandler.Instance.GetUpgradeValue(UpgradeType.ProjectileRange));
        }

        nextFireTime = Time.time + currentWeapon.fireRate;
    }
}
