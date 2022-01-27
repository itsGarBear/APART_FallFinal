using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rabbit : MonoBehaviour
{
    public GameObject target;
    private Player player;
    public float launchForce = 15f;
    Rigidbody2D rb;
    Vector3 startPos;
    public float closeEnoughX = 5f;

    [SerializeField] private LayerMask groundLayer;

    public float detectPlayerRadius = 10f;
    public float timeToNextJump = 2f;
    private float timer = 0f;
    private bool didLaunch = false;

    public int rabbitDamage;

    private Animator anim;
    private Nullable<float> animSpeed;
    


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().gameObject;
        player = target.GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    public void Launch(float force)
    {
        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = new Vector3();
        
        aimVector = fs.Calculate(transform.position, target.transform.position, force, Physics.gravity);
        animSpeed = fs.timeToTarget;

        if(animSpeed == null)
        {
            animSpeed = 1;
        }
        else
        {
            animSpeed = 1 / fs.timeToTarget;
        }

        if (aimVector == null)
        {
            if (target.transform.position.x < transform.position.x)
            {
                closeEnoughX *= -1f;
            }

            Vector3 movePos = new Vector3(closeEnoughX, target.transform.position.y, target.transform.position.z);
            aimVector = fs.Calculate(transform.position, movePos, force, Physics.gravity);
            
        }

        if(aimVector.HasValue)
            rb.AddForce(aimVector.Value.normalized * force, ForceMode2D.Impulse);

        if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        closeEnoughX = Mathf.Abs(closeEnoughX);

    }
    void Update()
    {
        if (!PlayerController.myPlayer.isCara)
        {
            if (!didLaunch && Mathf.Abs(transform.position.x - target.transform.position.x) < detectPlayerRadius)
            {
                didLaunch = true;
                anim.SetBool("Jump", true);

                Launch(launchForce);
                anim.SetFloat("Speed", (float)animSpeed);

            }

            if (didLaunch)
            {
                timer += Time.deltaTime;
                if (timer >= timeToNextJump)
                {
                    anim.SetFloat("Speed", 1f);
                    anim.SetBool("Jump", false);
                    timer = 0f;
                    didLaunch = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (collision.gameObject.GetComponent<Bullet>().isLethal)
                Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if (!PlayerController.myPlayer.isCara)
            {
                print("damage alice");
                player.Damage(rabbitDamage);
            }
                
        }
    }
}
