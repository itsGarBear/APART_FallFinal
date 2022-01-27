using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodentSpawner : MonoBehaviour
{
    [HideInInspector] public static RodentSpawner instance;

    [Header("Rodent Prefabs")]
    public GameObject ratPrefab;
    public GameObject rabbitPrefab;

    [Header("Spawns")]
    public List<Transform> ratSpawns;
    public List<Transform> rabbitSpawns;

    [Header("Rodent Spawns Used")]
    [SerializeField] List<Transform> ratSpawnsUsed = new List<Transform>();
    [SerializeField] List<Transform> rabbitSpawnsUsed = new List<Transform>();

    [Header("Numbers To Spawn")]
    public int numberOfRatsToSpawn;
    public int numberOfRabbitsToSpawn;

    private void Start()
    {
        instance = this;
    }
    public void SpawnAnimals()
    {
        ratSpawnsUsed = new List<Transform>();
        rabbitSpawnsUsed = new List<Transform>();

        for (int i = 0; i < numberOfRatsToSpawn; i++)
        {
            int randomNumber = Random.Range(0, ratSpawns.Count);
            if (!ratSpawnsUsed.Contains(ratSpawns[randomNumber]))
            {
                ratSpawnsUsed.Add(ratSpawns[randomNumber]);
            }
            else
            {
                i -= 1;
            }
        }

        for(int i = 0; i < numberOfRabbitsToSpawn; i++)
        {
            int randomNumber = Random.Range(0, rabbitSpawns.Count);
            if (!rabbitSpawnsUsed.Contains(rabbitSpawns[randomNumber]))
            {
                rabbitSpawnsUsed.Add(rabbitSpawns[randomNumber]);
            }
            else
            {
                i -= 1;
            }
        }
        
        foreach(Transform t in ratSpawnsUsed)
        {
            Instantiate(ratPrefab, t);
        }

        foreach(Transform t in rabbitSpawnsUsed)
        {
            Instantiate(rabbitPrefab, t);
        }

    }
}
