using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
