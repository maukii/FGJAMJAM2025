using UnityEngine;

public class Enemy_Teleporter : EnemyController
{
    [Header("Assign this prefab in inspector")]
    public GameObject ProjectilePrefab;

    float teleportTimer = 0f;
    [Header("Teleport variables")]
    [SerializeField] float teleportInterval = 5f;
    [SerializeField] float teleportDistance = 5f;

    float attackTimer = 0f;
    float attackInterval = 3f;

    bool initialTeleport = false;

    protected override void Update()
    {
        teleportTimer += Time.deltaTime;
        if(teleportTimer >= teleportInterval)
        {

        }
        base.Update();
    }

    protected override bool HandleMovement()
    {
        // Teleport at spawn
        if(!initialTeleport)
        {
            initialTeleport = true;
            Teleport();
        }

        if(teleportTimer >= teleportInterval)
        {
            teleportTimer = 0f;
            Teleport();
        }

        return false;
    }

    protected override void HandleRotation()
    {
        Vector3 targetDir = target - transform.position;
        float singleStep = rSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        newDir.y = 0f;
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void Teleport()
    {
        //Debug.Log(gameObject.name + " teleported");
        Vector2 unitCircle = Random.insideUnitCircle.normalized;
        Vector3 randomDir = Vector3.zero;
        randomDir.x = unitCircle.x;
        randomDir.z = unitCircle.y;
        newPos = target + randomDir * teleportDistance;

        // Reset attack timer on teleport
        attackTimer = 0f;
    }

    protected override bool CanAttack()
    {
        // Update attack cooldown, return true if it is time to attack
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            return true;
        }

        return false;
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject go = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = go.GetComponent<Projectile>();
        
        var rb = go.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = transform.forward * 5f;

        //projectile.Initialize(LayerMask.NameToLayer("Player"), 1, 5f, transform.forward);
    }
}
