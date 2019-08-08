using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialComplete : MonoBehaviour
{
    public Text Instruction;
    string mission =
        "Alright, now that you know the basics, \n" +
        "Best Of Luck and SHOW 'EM WHO'S BOSS!!! \n" +
        "[Space] to proceed.";

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        SceneManager.LoadScene("PrototypeScene");
    }

    void OnCollisionEnter(Collision collision)
    {
        TextChange();
    }

    void TextChange()
    {
        Instruction.text = mission;
    }
}
