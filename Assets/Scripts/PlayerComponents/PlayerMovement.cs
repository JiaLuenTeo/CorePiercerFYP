using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    public float dashTime = 1.0f;
    Rigidbody rb;
    float translationX, translationY;
    public float curTime;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        transform.Translate(new Vector3(translationX, 0f, translationY) * Time.deltaTime);
    }

    void dashMovement()
    {
        curTime += Time.deltaTime;
        Vector3 currentDirection = new Vector3(translationX, 0.0f, translationY);
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentDirection != Vector3.zero)
        {
            Debug.Log("Button Pressed");
            float buttonPressed = curTime;
            //play start up animation here

            //get button that is being pressed here for direction (y is set to the current player Y. Only moves in the XZ)
            
    
            if ((buttonPressed - curTime) <= dashTime)
            {
                PlayerManager.Instance.isInvincible = true;
                //Play dash animation here or something lol
                this.GetComponent<Rigidbody>().AddForce(currentDirection.normalized * dashDistance,ForceMode.Impulse);
                Debug.Log("Currently Dashing");
                curTime = 0.0f;
            }
            
            
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(curTime <= 0)
        PlayerManager.Instance.isInvincible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Walls")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
