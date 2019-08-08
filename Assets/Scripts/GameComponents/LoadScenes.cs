using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private static LoadScenes mInstance = null;
    public static LoadScenes Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.curState = CurrentGameState.MainMenu;
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.curGameTime = 0f;
        SceneManager.LoadScene("GameScene");
        GameManager.Instance.gameStarted();
        //GameManager.Instance.curState = CurrentGameState.GameStarted;
    }

    public void LoadTutorial()
    {
        GameManager.Instance.curState = CurrentGameState.GameTutorial;
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void afterGame()
    {
        SceneManager.LoadScene("End Screen");
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
