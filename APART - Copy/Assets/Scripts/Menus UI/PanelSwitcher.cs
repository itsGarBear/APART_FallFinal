using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public List<GameObject> panelsToSwap;
    public void ShowPanel(GameObject panel)
    {
        foreach (GameObject go in panelsToSwap)
            go.SetActive(false);
        panel.SetActive(true);
    }
}
