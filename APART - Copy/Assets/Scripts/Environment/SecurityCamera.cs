using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public DetectPlayerInLight myLight;
    public float rotateSpeed = 60f;
    public float completeRotationArc = 90f;
    public float offsetAngle = 45f;

    public bool canSeePlayer = false;
    public bool isDisabled = false;
    private float disabledTimer = 3f;
    private float disabledTimerCounter = 5f;

    public float playerSpeedMovePenalty = 20f;

    private PlayerController player;

    [SerializeField]
    private Transform pivotPoint;

    float timer = 0f;

    public GuardSpawner guardSpawner;

    private void Update()
    {
        if(!isDisabled)
        {
            if (!canSeePlayer)
            {
                timer += Time.deltaTime;
                pivotPoint.localEulerAngles = new Vector3(Mathf.PingPong(timer * rotateSpeed, completeRotationArc) - (offsetAngle - 90f), 90f, 90f);
            }
            else
            {
                //pivotPoint.LookAt(player.transform.position, transform.forward);
            }
        }
        else
        {
            disabledTimerCounter -= Time.deltaTime;
            myLight.DisableLight();
            if (disabledTimerCounter <= 0)
            {
                disabledTimerCounter = disabledTimer;
                myLight.EnableLight();
                isDisabled = false;
            }
        }
        
    }

    public void FoundPlayer(PlayerController seenPlayer)
    {
        canSeePlayer = true;
        player = seenPlayer;
        seenPlayer.moveSpeed = playerSpeedMovePenalty;

        //fire event for guards to chase player maybe
    }

    public void LostPlayer(PlayerController lostPlayer)
    {
        canSeePlayer = false;
        lostPlayer.moveSpeed = lostPlayer.maxMoveSpeed;
    }

    public void SpawnGuard()
    {
        guardSpawner.SpawnGuard();
    }


    //in order for the camera to continue rotating how it was, gotta use own timer instead of Time.time
    /*IEnumerator RotateCamera()
    {
        print("hy");
        timer += Time.deltaTime;
        
        

        yield return null;
    }*/
}
