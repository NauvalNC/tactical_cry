using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySpawner : MonoBehaviour
{
    public GameObject[] supplies;
    public int spawnPosAmount = 100;
    public float spawnPosRange = 5f;
    public float spawnDelay = 1f;
    List<Vector3> spawnPos = new List<Vector3>();

    private void Start()
    {
        GeneratePoint();
        StartCoroutine(Spawn());
    }

    void GeneratePoint() 
    {
        float offset = 0;
        float start = transform.position.x;
        for (int i = 0; i < spawnPosAmount; i++) 
        {
            spawnPos.Add(new Vector3(start + offset, transform.position.y, transform.position.z));
            offset += spawnPosRange;
        }
    }

    IEnumerator Spawn() 
    {
        while(true) 
        {
            yield return new WaitForSeconds(spawnDelay);
            int index = Random.Range(0, 2);
            int random = Random.Range(0, spawnPosAmount);
            Instantiate(supplies[index], spawnPos[random], Quaternion.identity);
        }
    }
}
