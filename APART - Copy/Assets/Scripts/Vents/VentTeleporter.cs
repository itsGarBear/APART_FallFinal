using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentTeleporter : MonoBehaviour
{
    [SerializeField] Vent myVent;
    [SerializeField] VentCanvas ventCanvas;
    public void Teleport()
    {
        print("clicked: " + transform.name);
        PlayerController.myPlayer.transform.position = myVent.exitPoint.position;
        ventCanvas.Disable();
    }
}
