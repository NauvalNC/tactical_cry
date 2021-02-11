using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]
    public static float reduceTime = 0.1f;
    int currentWave = 0;

    public GameObject[] enemies;
    public bool stopSpawn = false;
    public float spawnDelay = 2f;
    float delay;

    public int startAtWave = 1;
    bool spawnActive = false;

    private void Start()
    {
        currentWave = GameManager.Instance.waveCount;
        delay = spawnDelay;
    }

    private void Update()
    {
        if (spawnActive) return;

        if (GameManager.Instance.waveCount >= startAtWave) 
        {
            spawnActive = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        while(!stopSpawn) 
        {
            int rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], transform.position, Quaternion.identity).GetComponent<Enemy>();

            if (currentWave != GameManager.Instance.waveCount)
            {
                currentWave = GameManager.Instance.waveCount;
                delay -= reduceTime;
                if (delay <= 0) delay = 0;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
