using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyTypes;
    [SerializeField] private float timer;
    [SerializeField] private float enemySpawnInterval;
    [SerializeField] List<EnemyController> EnemyList = new List<EnemyController>();
    public EnemyController[] EnemyControllers { get { return EnemyList.ToArray(); } }

    public void SpawnEnemy(int enemyType, Vector3 position)
    {
        Vector3 pos = position;
        pos.x += Random.Range(-0.1f, 0.1f);
        pos.z += Random.Range(-0.1f, 0.1f);
        GameObject go = Instantiate(EnemyTypes[enemyType], pos, Quaternion.identity, transform);
        EnemyController em = go.GetComponent<EnemyController>();
        em.Initialize(this);
        EnemyList.Add(em);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        EnemyList.Remove(enemy);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > enemySpawnInterval)
        {
            timer -= enemySpawnInterval;
            SpawnEnemy(0, transform.position);
        }
    }
}
