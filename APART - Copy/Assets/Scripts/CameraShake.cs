using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //shout out brackeys o7
    //EZ CAMERA SHAKER ASSET 
    //https://youtu.be/9A9yj8KnM8c?t=441

    public bool canCameraShake = true;

    public void ToggleCamShake()
    {
        canCameraShake = !canCameraShake;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        if(canCameraShake)
        {
            Vector3 ogPos = transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, ogPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = ogPos;
        }
        
    }
}
