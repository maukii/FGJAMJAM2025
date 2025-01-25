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
            rb.linearVelocity = bulletSpawnPoint.forward * currentWeapon.projectileSpeed;

        nextFireTime = Time.time + currentWeapon.fireRate;
    }
}
