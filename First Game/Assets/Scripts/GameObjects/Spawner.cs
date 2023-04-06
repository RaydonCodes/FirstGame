using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] enemiesToSpawn;
    [Tooltip("Randomly spawn between first and second value")]
    public Vector2 timesBetweenSpawns;
    public int amountOfSpawns;
    public float spawnForce;

    float timer;
    float timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = Random.Range(timesBetweenSpawns.x, timesBetweenSpawns.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        if (timer > timeToSpawn)
        {
            timer = 0;
            for(int i = 1; i <= amountOfSpawns; i++)
            {
                SpawnEnemy();
            } 
        }
        
    }

    void SpawnEnemy()
    {
        GameObject enemySpawned = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], transform.position, Quaternion.identity);
        Rigidbody2D enemyRb = enemySpawned.GetComponent<Rigidbody2D>();
        WalkingEnemy enemyScript = enemySpawned.GetComponent<WalkingEnemy>();
        enemyScript.cancelMovement = true;
        
        int direction = Random.Range(-1, 2);
        if(direction == 0)
        {
            direction = -1;
        }
        enemyRb.AddForce(new Vector2(direction * spawnForce, spawnForce), ForceMode2D.Impulse);
        StartCoroutine(enemyScript.FollowPlayerWhenTouchGround());
    }
}
