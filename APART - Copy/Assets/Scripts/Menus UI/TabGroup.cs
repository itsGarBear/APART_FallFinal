using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public Color black = Color.black;
    public Color white = Color.white;

    public PanelGroup panelGroup;

    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    //public List<TextMeshProUGUI> tabText;

    private void Start()
    {
        OnTabSelected(tabButtons[0]);
    }
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;

        
        ResetTabs();
        button.background.color = tabActive;
        //button.GetComponent<TextMeshProUGUI>().color = white;

        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }

            button.background.color = tabIdle;
            //button.GetComponent<TextMeshProUGUI>().color = black;

        }
    }
}
