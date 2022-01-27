using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public List<MapElement> mapElements;

    public static MiniMap instance;
    private void Start()
    {
        instance = this;
    }

    //call when character switch, need to save once we can go back and forth
    public void ResetMiniMap()
    {
        foreach(MapElement m in mapElements)
        {
            m.isFound = false;
            m.GetComponent<BoxCollider2D>().enabled = true;
            m.unlockZone.gameObject.SetActive(false);

        }
    }
}

