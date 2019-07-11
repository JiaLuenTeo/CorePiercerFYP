using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPractise : MonoBehaviour
{
    public GameObject wall;
    public Text Instruction;
    string mission =
        "You have the weapon in your arsenal! \n" + 
        "[Left mouse click] to shoot.";

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        TextChange();
        MoveWall();
    }

    void TextChange()
    {
        Instruction.text = mission;
    }

    void MoveWall()
    {
        Vector3 newPos = new Vector3(0f, 0f, -7.5f);
        wall.transform.position = newPos;
    }
}