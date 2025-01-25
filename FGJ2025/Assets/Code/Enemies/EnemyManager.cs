using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyTypes;
    [SerializeField] private float timer;
    [SerializeField] private float enemySpawnInterval;
    [SerializeField] List<EnemyController> EnemyList = new List<EnemyController>();
    [SerializeField] int enemyCount = 0;
    public EnemyController[] EnemyControllers { get { return EnemyList.ToArray(); } }

    float minX = -50, maxX;
    float minY = -50, maxY;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > enemySpawnInterval)
        {
            timer -= enemySpawnInterval;
            SpawnEnemy(0, RandomOffscreenPos());
        }
    }

    public void SpawnEnemy(int enemyType, Vector3 position)
    {
        Vector3 pos = position;
        pos.x += Random.Range(-0.1f, 0.1f);
        pos.z += Random.Range(-0.1f, 0.1f);
        GameObject go = Instantiate(EnemyTypes[enemyType], pos, Quaternion.identity, transform);
        EnemyController em = go.GetComponent<EnemyController>();
        em.Initialize(this);
        go.name = go.name + "(" + enemyCount + ")";
        enemyCount++;
        EnemyList.Add(em);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        EnemyList.Remove(enemy);
    }

    private Vector3 RandomOffscreenPos()
    {
        Vector3 returnPos = Vector3.zero;
        int rand = Random.Range(0, 4);
        maxX = Screen.width + 50;
        maxY = Screen.height + 50;
        float x = 0;
        float y= 0;
        switch(rand)
        {
            case 0:
                x = 0;
                y = Random.Range(minY, maxY);
                break;
            case 1:
                x = Random.Range(minX, maxX);
                y = Screen.height;
                break;
            case 2:
                x = Screen.width;
                y = Random.Range(minY, maxY);
                break;
            case 3:
                x = Random.Range(minX, maxX);
                y = 0;
                break;
        }
        Vector3 pos = new Vector3(x, y, 0);


        //Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("hit.point = " + hit.point);
            returnPos = hit.point;
            //Vector3 lookDirection = targetPoint - transform.position;
            //lookDirection.y = 0f;
            //Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        return returnPos;
        //Debug.Log(pos);
        //return Camera.main.ScreenToWorldPoint(pos);
    }
}
