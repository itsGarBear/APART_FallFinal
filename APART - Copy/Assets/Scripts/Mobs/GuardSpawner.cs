using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour
{
    public Guard guardPrefab;

    public float delayBetweenSpawns;
    public bool canSpawnAgain = true;
    private float timer = 0f;

    public void SpawnGuard()
    {
        if(canSpawnAgain)
        {
            canSpawnAgain = false;
            Instantiate(guardPrefab, transform);
        }
            
    }

    private void Update()
    {
        if(!canSpawnAgain)
        {
            timer += Time.deltaTime;
            if(timer > delayBetweenSpawns)
            {
                canSpawnAgain = true;
                timer = 0f;
            }
        }
    }
}
