using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialSequence : MonoBehaviour
{
    public Text Instruction;
    int buttonCount;
    public GameObject wall;

    public static TutorialSequence instance;

    public enum TutorialState
    {
        MOVEMVENT,
        SHOOT,
        DODGE,
        COMPLETE
    }

    [Header("Tutorial State")]
    public TutorialState currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TutorialState.MOVEMVENT:
                MovementStage();
                break;
            case TutorialState.SHOOT:
                HitTheTargets();
                break;
            case TutorialState.DODGE:
                DodgeTheIncoming();
                break;
            case TutorialState.COMPLETE:
                Onward();
                break;
        }
    }

    void MovementStage()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            buttonCount++;
        }

        if (buttonCount > 4)
        {
            string mission = "That's just a basic of basics. \n" +
                "Move on to the next checkpoint!";

            Instruction.text = mission;
        }
           
    }

    void HitTheTargets()
    {
        string mission = "You have a weapon in your arsenal! \n" +
            "Left mouse click to shoot";

        Instruction.text = mission;

        if(GameObject.FindGameObjectsWithTag("Targets").Length == 0)
        {
            Vector3 newPos = new Vector3(0f, 0f, -7.5f);
            wall.transform.position = newPos;

            mission = "Lock n' loaded, oh yeah! \n" +
                "Move on to the next checkpoint!";

            Instruction.text = mission;
        }
    }

    void DodgeTheIncoming()
    {
        string mission = "Sometimes you need to dodge incoming attacks from geiing hurt! \n" +
            "[Left Shift] to dash";

        Instruction.text = mission;
    }

    void Onward()
    {
        string mission = "Alright, you now have what it tkaes to bring the big bad boss down!! \n" +
            "[Space] to proceed.";

        Instruction.text = mission;

        if (Input.GetKeyDown(KeyCode.Space) && currentState == TutorialState.COMPLETE)
        {
            LoadScenes.Instance.LoadGame();
        }
    }
}
