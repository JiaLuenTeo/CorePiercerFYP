using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkGameText : MonoBehaviour
{

    public Text timeOrLost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.curState == CurrentGameState.GameLost)
        {
            timeOrLost.text = "You Lost! Try again next time!";
        }
        else if (GameManager.Instance.curState == CurrentGameState.GameWon)
        {
            timeOrLost.text = "Your time is : " + GameManager.Instance.curGameTime.ToString("F2") + ". Congrats!";
        }
    }
}
