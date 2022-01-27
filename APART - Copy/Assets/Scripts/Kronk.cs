using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kronk : MonoBehaviour
{
    private Animator anim;
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
        anim = GetComponent<Animator>();
    }
    private void OnMouseOver()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            anim.SetBool("PullLever", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }    
    }

    public void LeverDown()
    {
        PlayerSwitcher.instance.LeverPulled();
    }
}
