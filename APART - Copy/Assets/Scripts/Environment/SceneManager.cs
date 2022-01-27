using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [HideInInspector]
    public SceneManager instance;

    private void Awake() { instance = this; }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.ExitPlaymode();
    }
}
