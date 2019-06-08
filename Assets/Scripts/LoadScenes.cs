using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
