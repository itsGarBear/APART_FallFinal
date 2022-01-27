using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject playerHealth;
    public GameObject playerInv;

    public bool isPaused;
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingsMenu.activeSelf)
                pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }

        if (pauseMenu.activeSelf || settingsMenu.activeSelf)
        {
            isPaused = true;
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Menu")
            {
                playerHealth.SetActive(false);
                playerInv.SetActive(false);
            }
            TimeScaler.instance.PauseTime();
        }
        else if(!TimeScaler.instance.canvas.activeSelf)
        {
            isPaused = false;
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Menu")
            {
                playerHealth.SetActive(true);
                playerInv.SetActive(true);
            }
            TimeScaler.instance.ResumeTime();
        }
    }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
