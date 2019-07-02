using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    Rigidbody rb;
    float translationX, translationY;
    float curTime;
    public bool disableClip = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        keyboardMovement();
        if (!disableClip)
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Button Pressed");
            float buttonPressed = curTime;
            //play start up animation here

            //get button that is being pressed here for direction (y is set to the current player Y. Only moves in the XZ)
            Vector3 currentDirection = new Vector3(translationX, 0.0f, translationY);

            RaycastHit hit;
            Ray ray = new Ray(transform.position, currentDirection);

            if (Physics.Raycast(ray, out hit, dashDistance))
            {
                if (hit.transform.tag == "Walls" && (buttonPressed - curTime) <= 0.1f)
                {
                    PlayerManager.Instance.isInvincible = true;
                    if (transform.position != hit.point)
                    {
                        transform.Translate(currentDirection.normalized * dashDistance * Time.deltaTime);
                    }
                   
                }
                else if ((buttonPressed - curTime) <= 0.1f)
                {
                    PlayerManager.Instance.isInvincible = true;
                    transform.Translate(currentDirection.normalized * dashDistance * Time.deltaTime);
                }
            }
    
            /*if ((buttonPressed - curTime) <= 0.1f)
            {
                PlayerManager.Instance.isInvincible = true;
                //Play dash animation here or something lol
                transform.Translate(currentDirection.normalized * dashDistance * Time.deltaTime);
                Debug.Log("Currently Dashing");
            }*/
            
        }
        PlayerManager.Instance.isInvincible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Walls")
        {
            disableClip = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Walls")
        {
            disableClip = false;
        }
    }
}
