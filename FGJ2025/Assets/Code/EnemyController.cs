using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyManager enemyManager;

    // For distance calculations
    Vector3 offset;
    float sqrLen;
    float closeDist = 1f;

    Vector3 newPos;

    public void Initialize(EnemyManager em)
    {
        enemyManager = em;
    }

    public void Die()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    void Update()
    {
        newPos = transform.position;

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
                Debug.Log(gameObject.name + ": sqrLen: " + sqrLen + " < radius*radius:" + closeDist*closeDist);
                newPos += (transform.position - em.transform.position) * Time.deltaTime;
                //transform.Translate((transform.position - em.transform.position) * Time.deltaTime);
            }
        }

        // Set Y to 0 so the enemy is never under or above ground level
        newPos.y = 0;
        transform.position = newPos;
    }
}
