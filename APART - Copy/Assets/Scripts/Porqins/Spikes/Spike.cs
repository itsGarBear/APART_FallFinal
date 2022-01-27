using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int spikeDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Damage(spikeDamage);
        }
    }
}
