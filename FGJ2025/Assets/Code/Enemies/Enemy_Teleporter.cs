using UnityEngine;

public class Enemy_Teleporter : EnemyController
{
    float teleportTimer = 0f;
    [SerializeField] float teleportInterval = 5f;
    [SerializeField] float teleportDistance = 5f;

    bool initialTeleport = false;

    protected override void Update()
    {
        teleportTimer += Time.deltaTime;
        if(teleportTimer >= teleportInterval)
        {

        }
        base.Update();
    }

    protected override void HandleMovement()
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
    }
}
