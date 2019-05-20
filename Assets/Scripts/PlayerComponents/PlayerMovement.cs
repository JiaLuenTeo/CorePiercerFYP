using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    float translationX, translationY;
    float curTime;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyboardMovement();
        dashMovement();
    }

    void keyboardMovement()
    {
        translationY = Input.GetAxis("Vertical") * speed;
        translationX = Input.GetAxis("Horizontal") * speed;

        transform.Translate(new Vector3(translationX, 0f, translationY) * Time.deltaTime, Space.World);
    }

    void dashMovement()
    {
        curTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Button Pressed");
            float buttonPressed = curTime;
            //play start up animation here

            //get button that is being pressed here for direction (y is set to the current player Y. Only moves in the XZ)
            Vector3 currentDirection = new Vector3(translationX, 0.0f, translationY);


            if ((buttonPressed - curTime) <= 0.1f)
            {
                PlayerManager.Instance.isInvincible = true;
                //Play dash animation here or something lol
                transform.Translate(currentDirection.normalized * dashDistance * Time.deltaTime);
                Debug.Log("Currently Dashing");
            }
            
        }
        PlayerManager.Instance.isInvincible = false;
    }
}
