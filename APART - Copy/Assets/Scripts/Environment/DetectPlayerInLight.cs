using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerInLight : MonoBehaviour
{
    public Material cannotSeePlayerColor;
    public Material canSeePlayerColor;
    public Material disabledColor;

    public SecurityCamera myCamera;

    PlayerController player;

    public void DisableLight()
    {
        GetComponent<SpriteRenderer>().material = disabledColor;
    }
    public void EnableLight()
    {
        GetComponent<SpriteRenderer>().material = cannotSeePlayerColor;
    }


    //Can see player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!myCamera.isDisabled)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (PlayerController.myPlayer.isCara)
                {
                    GetComponent<SpriteRenderer>().material = canSeePlayerColor;
                    player = collision.gameObject.transform.root.GetComponent<PlayerController>();
                    myCamera.FoundPlayer(player);
                }
                
            }
        }
        
    }

    //Cannot see player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!myCamera.isDisabled)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (PlayerController.myPlayer.isCara)
                {
                    GetComponent<SpriteRenderer>().material = cannotSeePlayerColor;
                    player = collision.gameObject.transform.root.GetComponent<PlayerController>();
                    myCamera.LostPlayer(player);
                }
                
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!myCamera.isDisabled)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                if (PlayerController.myPlayer.isCara)
                {
                    myCamera.SpawnGuard();
                }
                    
            }
        }
    }
}
