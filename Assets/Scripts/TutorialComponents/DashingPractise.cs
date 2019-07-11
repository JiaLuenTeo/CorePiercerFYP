using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashingPractise : MonoBehaviour
{
    public Text Instruction;
    string mission =
        "Sometimes you need to dodge to avoid getting hurt! \n" +
        "[Left Shift] to dash/dodge.";

    void Start()
    {

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
