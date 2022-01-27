using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapElement : MonoBehaviour
{
    public Image unlockZone;
    public bool isFound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isFound = true;
            unlockZone.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        } 
    }
}
