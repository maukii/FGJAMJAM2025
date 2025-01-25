using UnityEngine;

public class Enemy_Basic : EnemyController
{
    [SerializeField] float attackDistance = 1f;
    protected override void HandleRotation()
    {
        Vector3 targetDir = target - transform.position;
        float singleStep = rSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        newDir.y = 0f;
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    protected override void HandleMovement()
    {
        newPos += transform.forward * mSpeed * Time.deltaTime;
    }

    protected override bool CanAttack()
    {
        if(Vector3.Distance(transform.position, target) <= attackDistance)
        {
            return true;
        }

        return false;
    }

    protected override void Attack()
    {
        // Play attack animation, player takes damage
    }
}
