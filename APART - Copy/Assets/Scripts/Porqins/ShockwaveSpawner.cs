using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    public Shockwave shockwave;
    public static ShockwaveSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnShockWave()
    {
        Shockwave sW = Instantiate(shockwave, transform);
        sW.StartScaling();
    }
}
