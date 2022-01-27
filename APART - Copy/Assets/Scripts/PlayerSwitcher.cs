using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSwitcher : MonoBehaviour
{
    public Animator fadeToBlack;

    public GameObject minimap;
    public GameObject ventMap;
    public Inventory inventory;

    public GameObject closedRodentCages;
    public GameObject porqinsUseless;
    public GameObject porqinsActual;

    public static PlayerSwitcher instance;

    public TextMeshProUGUI cornerObjText;

    public GameObject playerCanvas;


    private void Start()
    {
        instance = this;
    }
    public void LeverPulled()
    {
        fadeToBlack.SetBool("FadeIn", true);
        TimeScaler.instance.didCaraSequence = true;
    }
    public void FinishedFade()
    {
        //RESET IT ALL
        TimeScaler.instance.StopTime();

        PlayerController.myPlayer.isCara = false;
        PlayerController.myPlayer.transform.position = transform.root.position;

        PlayerController.myPlayer.SwitchCharacterSprites();

        PlayerController.myPlayer.gameObject.GetComponent<Player>().Heal(20);

        minimap.GetComponent<MiniMap>().ResetMiniMap();
        ventMap.GetComponent<VentCanvas>().ResetVentMaps();

        cornerObjText.text = "<b>Objective:</b>\nFind Porqins and deal with him";

        closedRodentCages.SetActive(false);
        porqinsUseless.SetActive(false);
        playerCanvas.SetActive(false);
        porqinsActual.SetActive(true);
        RodentSpawner.instance.SpawnAnimals();

        inventory.ResetInventory();

        

    }

    public void FadeToGame()
    {
        fadeToBlack.SetBool("FadeIn", false);
        fadeToBlack.SetBool("FadeOut", true);


        TimeScaler.instance.StartTime();
        playerCanvas.SetActive(true);
        //give all ammo
    }

    public void StartAlice()
    {
        print("alice turn");
    }

    public void AnimationComplete(string name)
    {
        fadeToBlack.SetBool(name, false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LeverPulled();
        }
    }
}
