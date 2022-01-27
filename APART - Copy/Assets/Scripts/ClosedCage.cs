using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedCage : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public RodentSpawner rodentSpawner;
    public SpawnRodentType rodentType;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void FreeRodent()
    {
        if (rodentType == SpawnRodentType.Rat) rodentSpawner.numberOfRatsToSpawn++;
        else if (rodentType == SpawnRodentType.Rabbit) rodentSpawner.numberOfRabbitsToSpawn++;

        boxCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            FreeRodent();
        }
    }
}

public enum SpawnRodentType
{
    Rat,
    Rabbit
}
