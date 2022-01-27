using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public Stairs stairExitsAt;

    public Vector3 EnterStair()
    {
        return stairExitsAt.transform.GetChild(0).position;
    }
}
