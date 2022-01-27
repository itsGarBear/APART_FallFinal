using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PorqinsAnimationManager : MonoBehaviour
{
    public Animator animator;

    public CameraShake cameraShake;

    public Toggle camShakeToggle;

    public bool canRun = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //hey a shockwave would be kinda cool
    //dodge projectiles and the sw
    public void SpikeLaunch()
    {
        animator.SetTrigger("SpikeLaunch");   
    }

    public void GroundPound()
    {
        animator.SetTrigger("GroundPound");
    }

    public void StartRunning()
    {
        animator.SetBool("Run", true);
    }
    public void StopRunning()
    {
        animator.SetBool("Run", false);
    }
    public void CameraShake()
    {
        if(camShakeToggle.isOn)
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
        }
        
    }

    public void AnimationComplete()
    {
        animator.SetTrigger("AnimationComplete");
        PorqinsBossFight.instance.PhaseAttacks();
    }
}
