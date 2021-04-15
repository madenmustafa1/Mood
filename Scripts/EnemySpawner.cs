using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyPos;

    private bool canSpawn;
    

    void Start()
    {
        canSpawn = true;
    }

    
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnEnemy");
        }
    }
    IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, enemyPos.transform.position,Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(5f);
        canSpawn = true;
    }
}
