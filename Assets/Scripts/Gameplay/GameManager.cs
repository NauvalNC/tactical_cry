using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get 
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public int killCount = 0;

    [Header("Wave Control")]
    int lastWave = 0;
    public Animator scoringAC;
    public AudioSource waveSFX;
    public int waveCount = 1;
    public int waveThershold = 10;
    public float enemySpawnReduceTimePerWave = 0.1f;

    private void Start()
    {
        EnemySpawner.reduceTime = enemySpawnReduceTimePerWave;
    }

    private void Update()
    {
        waveCount = (killCount / waveThershold) + 1;
        if (lastWave != waveCount) 
        {
            waveSFX.Play();
            scoringAC.Play("waveUp");
            lastWave = waveCount;
        }
    }
}
