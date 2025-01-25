using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    [Header("Assign these prefabs in the inspector")]
    public GameObject[] EnemyTypes;

    [Header("SerializeFields")]
    // Spawner variables
    [SerializeField] private float spawnTimer;
    [SerializeField] private float enemySpawnInterval;

    // For keeping track of enemies
    [SerializeField] List<EnemyController> EnemyList = new List<EnemyController>();
    [SerializeField] int totalEnemiesSpawned = 0;
    public int TotalEnemiesSpawned { get { return totalEnemiesSpawned; } }
    public EnemyController[] EnemyControllers { get { return EnemyList.ToArray(); } }

    // For screen positions
    float minX, maxX;
    float minY, maxY;

    GameObject playerGO;
    Health playerHealth;
    public GameObject PlayerGO { get { return playerGO; } }
    public Health PlayerHealth { get { return playerHealth; } }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > enemySpawnInterval)
        {
            spawnTimer -= enemySpawnInterval;
            SpawnEnemy(Random.Range(0, EnemyTypes.Length), RandomOffscreenPos());
        }
    }

    void Start()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerGO.GetComponent<Health>();
    }

    public void SpawnEnemy(int enemyType, Vector3 position)
    {
        Vector3 pos = position;
        pos.x += Random.Range(-0.1f, 0.1f);
        pos.z += Random.Range(-0.1f, 0.1f);
        GameObject go = Instantiate(EnemyTypes[enemyType], pos, Quaternion.identity, transform);
        EnemyController em = go.GetComponent<EnemyController>();
        em.Initialize(this);
        go.name = go.name + "(" + totalEnemiesSpawned + ")";
        totalEnemiesSpawned++;
        EnemyList.Add(em);
    }

    public void SpawnEnemies(int enemyType, int amount, Vector3 position)
    {
        for(int i = 0; i < amount; i++)
        {
            SpawnEnemy(enemyType, position);
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        EnemyList.Remove(enemy);
    }

    private Vector3 RandomOffscreenPos()
    {
        Vector3 returnPos = Vector3.zero;
        
        // Set minX and minY according to screen width and height, give 50px leeway
        minX = -50; minY = -50;
        maxX = Screen.width + 50;
        maxY = Screen.height + 50;

        // rand determines which side of the screen the enemy spawns in
        // 0: left
        // 1: top
        // 2: right
        // 3: bottom
        int rand = Random.Range(0, 4);

        // Vector that's passed to ScreenPointToRay to create a ray
        Vector3 screenRayPos = Vector3.zero;

        switch(rand)
        {
            case 0:
                screenRayPos.x = 0;
                screenRayPos.y = Random.Range(minY, maxY);
                break;
            case 1:
                screenRayPos.x = Random.Range(minX, maxX);
                screenRayPos.y = Screen.height;
                break;
            case 2:
                screenRayPos.x = Screen.width;
                screenRayPos.y = Random.Range(minY, maxY);
                break;
            case 3:
                screenRayPos.x = Random.Range(minX, maxX);
                screenRayPos.y = 0;
                break;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenRayPos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            //Debug.Log("hit.point = " + hit.point);
            returnPos = hit.point;
        }

        return returnPos;
    }
}
