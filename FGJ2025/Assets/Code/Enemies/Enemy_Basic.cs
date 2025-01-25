using UnityEngine;

public class Enemy_Basic : EnemyController
{
    protected override void HandleRotation()
    {
        Vector3 targetDir = target - transform.position;
        float singleStep = rSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    protected override void HandleMovement()
    {
        newPos += transform.forward * mSpeed * Time.deltaTime;
    }
}
