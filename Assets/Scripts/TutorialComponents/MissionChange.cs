using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionChange : MonoBehaviour
{
    public Text Instruction;
    public GameObject wall;
    string mission =
        "Go to the designated spot to commence the next instruction.";

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        TextChange();
        MoveWall();
    }

    void MoveWall()
    {
        Vector3 newPos = new Vector3(-10f, 0f, 2f);
        wall.transform.position = newPos;
    }

    void TextChange()
    {
        Instruction.text = mission;
    }
}
