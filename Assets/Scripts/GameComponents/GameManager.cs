using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CurrentGameState
{
    MainMenu,
    GameStarted,
    GameLost,
    GameWon,
    GamePaused,
    GameEnd
};

public class GameManager : MonoBehaviour
{
    private static GameManager mInstance = null;
    public static GameManager Instance;

    public CurrentGameState curState, preState;

    void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        curState = CurrentGameState.MainMenu;
        preState = curState;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
