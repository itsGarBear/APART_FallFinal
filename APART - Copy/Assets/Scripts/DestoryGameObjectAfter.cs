using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGameObjectAfter : MonoBehaviour
{
    public float timeToDestroy = 3f;
    public void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
