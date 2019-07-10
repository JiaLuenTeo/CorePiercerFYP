using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public float maxDistanceMousePlayer = 2.0f;
    Transform playerCharacter;
    Vector3 offset = new Vector3(0, 0, 10);
    Vector3 mouseOffset = new Vector3(0, 0, 10);
    Vector3 mouse;
    Vector3 pos;
    Vector3 velocity = new Vector3(1,0,1);
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = transform.position - playerCharacter.transform.position;

        //transform.position = playerCharacter.position - offset;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.TransformDirection(mouseOffset);

        
        if (Vector3.Distance(new Vector3(playerCharacter.position.x, 0.0f, playerCharacter.position.z), mouse) > maxDistanceMousePlayer)
        {
            pos = (playerCharacter.position + mouse) / 2f;
            transform.position = new Vector3(pos.x, 10.0f, pos.z);
        }
        else if (Vector3.Distance(new Vector3(playerCharacter.position.x, 0.0f, playerCharacter.position.z), mouse) <= maxDistanceMousePlayer)
        {
            transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(playerCharacter.transform.position.x, 10.0f, playerCharacter.transform.position.z), ref velocity, 0.1f);
        }
        
           
    }
}
