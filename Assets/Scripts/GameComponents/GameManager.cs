using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CurrentGameState
{
    MainMenu,
    GameTutorial,
    GameCutscene,
    GameCheckCS,
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
    public float curGameTime;

    public Animator mainCameraAnimator;

    void Awake()
    {
        
        GameObject.DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Object.Destroy(gameObject);
        
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
       if (curState == CurrentGameState.GameStarted) 
        {
            curGameTime += Time.deltaTime;
            PlayerManager.Instance.cutScene = false;
        }
       if (curState == CurrentGameState.GameCutscene)
        {
            BossAI.Instance.curState = BossAI.BossCurrentState.CUTSCENE;
            playCutscene();
        }
       if(curState == CurrentGameState.MainMenu)
        {
            curGameTime = 0.0f;
        }
       if (curState == CurrentGameState.GameCheckCS)
        {
            checkCS();
        }

       if (mainCameraAnimator == null)
        {
            mainCameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        }
    }

    public void gameStarted()
    {
        SoundManagerScript.Instance.StopBGM();
        curState = CurrentGameState.GameCutscene;
       
    }

    public void playCutscene()
    {
        if (mainCameraAnimator == null)
        {
            mainCameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        }
        mainCameraAnimator.SetBool("playBossIntro", true);
        PlayerManager.Instance.cutScene = true;
        curState = CurrentGameState.GameCheckCS;
    }

    public void checkCS()
    {
        if (!mainCameraAnimator.GetBool("playBossIntro"))
        {
            mainCameraAnimator.applyRootMotion = true;
            BossAI.Instance.curState = BossAI.BossCurrentState.SHOOTINGNORMAL;
            
            curState = CurrentGameState.GameStarted;
        }
    }

}
