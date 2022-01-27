using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public Animator animator;

    public void StartScaling()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Scale");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            print("hit player");
            collision.gameObject.GetComponent<Player>().Damage(8);
        }
    }
}
