using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentCanvas : MonoBehaviour
{
    public GameObject ventMapPanel;
    public GameObject map;

    [Header("GameObject Vent")]
    public List<Vent> vents;
    [Header("Canvas Representation")]
    public List<VentTeleporter> mapVents;

    

    public void Enable()
    {
        PlayerController.myPlayer.canShoot = false;
        foreach (Vent v in vents)
        {
            if(v.isUnlocked)
            {
                mapVents[vents.IndexOf(v)].gameObject.SetActive(true);
            }
        }

        map.SetActive(true);
        ventMapPanel.SetActive(true);
    }

    public void Disable()
    {
        map.SetActive(false);
        ventMapPanel.SetActive(false);
        PlayerController.myPlayer.canShoot = true;
    }

    private void LateUpdate()
    {
        //click outside the panel
        //if (ventMapPanel.activeSelf && Input.GetMouseButtonDown(0) && !RectTransformUtility.RectangleContainsScreenPoint(ventMapPanel.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        if (ventMapPanel.activeSelf && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M)))
        {
            Disable();
        }
    }

    public void ResetVentMaps()
    {
        foreach (Vent v in vents)
        {
            v.isUnlocked = false;
            v.boxCollider.enabled = false;
            v.sphereCollider.enabled = true;
        }
    }
}
