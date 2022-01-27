using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    public float xScaleFactor = 125f / 40f;
    public float yScaleFactor = 515 / 165;
    public GameObject map;
    void Update()
    {
        if(map.activeSelf)
        {
            //transform.localPosition = new Vector3(PlayerController.myPlayer.transform.position.x * xScaleFactor, 
            //    PlayerController.myPlayer.transform.position.y * yScaleFactor, 0f);
            float yContantPoint = PlayerController.myPlayer.circleCollider.bounds.center.y - PlayerController.myPlayer.circleCollider.bounds.extents.y;
            transform.localPosition = new Vector3(PlayerController.myPlayer.transform.localPosition.x * xScaleFactor, yContantPoint * yScaleFactor, 0f);
        }
    }
}
