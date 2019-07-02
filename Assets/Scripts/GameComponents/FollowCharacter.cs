using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    Transform playerCharacter;
    Vector3 offset = new Vector3(0, 0, 5);
    Vector3 mouseOffset = new Vector3(0, 0, 1);
    Vector3 mouse;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = transform.position - playerCharacter.transform.position;

        transform.position = playerCharacter.position - offset;

    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.TransformDirection(mouseOffset);

        transform.position = new Vector3(playerCharacter.transform.position.x, 10.0f, playerCharacter.transform.position.z);
        if (Vector3.Distance(new Vector3(playerCharacter.position.x, 0.0f, playerCharacter.position.z), mouse) > 10.0f)
        {
            pos = (playerCharacter.position + mouse) / 2f;
            transform.position = new Vector3(pos.x,10.0f, pos.z);
        }
           
    }
}
