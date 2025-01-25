using UnityEngine;

public class Enemy_Basic : EnemyController
{
    [SerializeField] float attackDistance = 1f;
    float cooldownTimer = 0f;
    float cooldownDuration = 1f;
    bool onCooldown = false;

    protected override void HandleRotation()
    {
        Vector3 targetDir = target - transform.position;
        float singleStep = rSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        newDir.y = 0f;
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    protected override bool HandleMovement()
    {
        // Can't move when in cooldown
        if(onCooldown)
        {
            return false;
        }
        newPos += transform.forward * mSpeed * Time.deltaTime;
        return true;
    }

    protected override bool CanAttack()
    {
        if(onCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer < cooldownDuration)
            {
                // If cooldownTimer is less than cooldown duration, can't attack
                return false;
            }else{
                // If cooldownTimer is past duration, no longer on cooldown
                onCooldown = false;
            }
        }

        if(Vector3.Distance(transform.position, target) <= attackDistance)
        {
            onCooldown = true;
            return true;
        }

        return false;
    }

    protected override void Attack()
    {
        base.Attack();
        onCooldown = true;
        cooldownTimer = 0f;
        enemyManager.PlayerHealth.TakeDamage(1);
    }
}
