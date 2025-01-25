using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyManager enemyManager;

    // For distance calculations
    Vector3 offset;
    float sqrLen;
    float closeDist = 1f;
    [SerializeField] float pushForce = 1f;

    // Calculate new distance into this before passing it into transform.position
    Vector3 newPos;

    Vector3 target = Vector3.zero;

    void Update()
    {
        newPos = transform.position;

        HandleEnemyCollisions();
        HandleRotation();
        HandleMovement();

        // Set Y to 0 so the enemy is never under or above ground level
        newPos.y = 0;
        transform.position = newPos;
    }
    
    public void Initialize(EnemyManager em)
    {
        enemyManager = em;
    }

    public void Die()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    void HandleEnemyCollisions()
    {
        // Optimization idea: Run this x amount of times per frame from EnemyManager, on x amount of EnemyControllers
        // x = 10, 60fps, 60enemies = 10*60 runs per second, 6 frames (.1 seconds) to run this on every enemy
        // Loop through every enemy and see if there's collisions
        foreach(EnemyController em in enemyManager.EnemyControllers)
        {
            if(em == this)
            {
                continue;
            }
            offset = em.transform.position - transform.position;
            sqrLen = offset.sqrMagnitude;
            if(sqrLen < closeDist * closeDist)
            {
                //Debug.Log("Collision between: " + gameObject.name + " and " + em.gameObject.name);
                newPos += (transform.position - em.transform.position).normalized * pushForce * Time.deltaTime;
            }
        }
    }

    void HandleRotation()
    {
        Vector3 targetDir = target - transform.position;
        float singleStep = 1f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void HandleMovement()
    {
        newPos += transform.forward * Time.deltaTime;
    }
}
