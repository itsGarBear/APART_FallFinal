using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorqinsManager : MonoBehaviour
{
    private Transform player;
    public int moveSpeed;
    public float followRadius;

    //Spikes
    [Header("Spikes")]
    public Transform[] spikeLaunchers;
    public GameObject spike;
    public float fireForce = 10f;

    //Animations
    private PorqinsAnimationManager porqinsAnimation;

    //Damage Flash
    [Header("Components")]
    private bool flashingDMG;

    public static PorqinsManager instance;

    public GameObject canvas;

    Color lethalColor = Color.red;
    Color nonLethalColor = new Color32(127, 88, 219, 255);
    private void Start()
    {
        instance = this;
        player = PlayerController.myPlayer.transform;
        porqinsAnimation = GetComponentInChildren<PorqinsAnimationManager>();
    }

    void Update()
    {
        if(porqinsAnimation.canRun)
        {
            if (checkIfPlayerInFollowRadius())
            {
                porqinsAnimation.StartRunning();
                if (player.position.x < transform.position.x)
                {
                    transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f);
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    canvas.transform.localScale = new Vector3(Mathf.Abs(canvas.transform.localScale.x), canvas.transform.localScale.y, canvas.transform.localScale.z);
                }
                else if (player.position.x > transform.position.x)
                {
                    transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    canvas.transform.localScale = new Vector3(-Mathf.Abs(canvas.transform.localScale.x), canvas.transform.localScale.y, canvas.transform.localScale.z);
                }
            }
            else
            {
                porqinsAnimation.StopRunning();
            }
        }
        else
        {
            porqinsAnimation.StopRunning();
        }
        
    }

    private bool checkIfPlayerInFollowRadius()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < followRadius)
            return true;
        else
            return false;
    }

    public void DamageFlash(SpriteRenderer[] spriteRenderers, bool isLethal)
    {
        if (flashingDMG)
            return;

        StartCoroutine(DamageFlashCoRoutine());

        IEnumerator DamageFlashCoRoutine()
        {
            flashingDMG = true;

            Color defaultColor = Color.white;
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                defaultColor = sr.material.color;
                if(isLethal)
                    sr.material.color = lethalColor;
                else
                    sr.material.color = nonLethalColor;
            }
            
            yield return new WaitForSeconds(0.1f);

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.material.color = defaultColor;
            }

            flashingDMG = false;
        }
    }
    
}
