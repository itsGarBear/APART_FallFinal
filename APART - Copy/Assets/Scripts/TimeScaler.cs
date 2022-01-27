using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeScaler : MonoBehaviour
{
    public static TimeScaler instance;
    public bool timeIsStopped = false;
    public bool didCaraSequence = false;

    public GameObject canvas;
    public GameObject playerCanvas;
    public TextMeshProUGUI lastWeekText;
    public TextMeshProUGUI currentObjText;
    public TextMeshProUGUI catbText;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        if(pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
        }
        if(settingsMenu == null)
        {
            settingsMenu = GameObject.Find("SettingsPanel");
            settingsMenu.SetActive(false);
        }


        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Menu")
        {
            if(pauseMenu != null && settingsMenu != null)
            {
                if (timeIsStopped && !pauseMenu.activeSelf && !settingsMenu.activeSelf && Input.GetMouseButtonDown(0))
                {
                    print("playing");
                    PlayerSwitcher.instance.FadeToGame();
                }
            }
            
        }
        
    }

    public void StopTime()
    {
        timeIsStopped = true;
        Time.timeScale = 0f;
        if (!didCaraSequence)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
            lastWeekText.text = "Last week's break-in was a success. You found and angered Porqins. His rage wreaked havoc on the building.";
            currentObjText.text = "This time... you want to free him.";
        }
        else
        {
            lastWeekText.text = "Last night's break-in has freed Porqins. He is now attempting to escape the building.";
            currentObjText.text = "Deal with this now!";
            PickupSpawner.instance.SpawnPickups();
        }

        canvas.SetActive(true);
        playerCanvas = GameObject.Find("PlayerCanvas");
    }
    public void StartTime()
    {
        canvas.SetActive(false);
        timeIsStopped = false;
        Time.timeScale = 1f;
    }

    public void PauseTime()
    {
        timeIsStopped = true;
        Time.timeScale = 0f;
    }
    public void ResumeTime()
    {
        timeIsStopped = false;
        Time.timeScale = 1f;
    }

    public void PlayerDead()
    {
        didCaraSequence = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void PorqinsDead(bool isLethal)
    {
        PauseTime();
        if(isLethal)
        {
            lastWeekText.text = "You have handled the problem, but lost years of research";
            currentObjText.text = "";
        }
        else
        {
            lastWeekText.text = "Congratulations Dr. Ardans, you successfully sedated Experiment PQ-141";
            currentObjText.text = "";
        }

        canvas.SetActive(true);
        playerCanvas.SetActive(false);
        catbText.text = "";
        Invoke("BackToMenu", 3f);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }


}
