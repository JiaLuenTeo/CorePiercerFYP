using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CurrentGameState
{
    MainMenu,
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
        else
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
            BossAI.Instance.curState = BossAI.BossCurrentState.SHOOTINGNORMAL;
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
        curState = CurrentGameState.GameCutscene;
    }

    public void playCutscene()
    {
        mainCameraAnimator.SetBool("playBossIntro", true);
        curState = CurrentGameState.GameCheckCS;
    }

    public void checkCS()
    {
        if (!mainCameraAnimator.GetBool("playBossIntro"))
        {
            mainCameraAnimator.applyRootMotion = true;
            curState = CurrentGameState.GameStarted;
        }
    }

}
