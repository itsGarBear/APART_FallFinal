using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    private bool onRightSide = true;
    private void Update()
    {
        if (!TimeScaler.instance.timeIsStopped)
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

        //if(onRightSide && (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270))
        //{
        //    print("left");
        //    float newZAngle = transform.rotation.eulerAngles.z * -1;
        //    float newYAngle = -180f;
        //    transform.eulerAngles = new Vector3(0f, newYAngle, newZAngle);
        //    onRightSide = false;
        //}

        //if(!onRightSide && (transform.rotation.eulerAngles.z < 90 || transform.rotation.eulerAngles.z > 270))
        //{
        //    print("right");
        //    float newZAngle = transform.rotation.eulerAngles.z;
        //    float newYAngle = 0f;
        //    transform.eulerAngles = new Vector3(0f, newYAngle, newZAngle);
        //    onRightSide = true;
        //}

    }
}
