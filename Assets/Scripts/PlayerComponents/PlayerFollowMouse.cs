using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{
    public Vector3 Pivot;

    public Transform gun;
    public Transform gunRotationGO;
    public Transform offsetR, offsetL;
    public Transform handR, handL;

    Vector3 mousePosition;
    public float angle;
    public float angleFix = 0;

    public bool isRight = true;
    public bool isLeft = false;
    public bool hasMovedR = true;
    public bool hasMovedL = false;

    // Start is called before the first frame update
    void Start()
    {
        FindDistance();
    }

    // Update is called once per frame
    void Update()
    {
        findMouseAngle();
        PivotAroundPoint();
        angleFixing();
    }

    void FindDistance()
    {
        Pivot.x = offsetR.position.x - gun.position.x;
    }

    void findMouseAngle()
    {
        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (mousePosition.sqrMagnitude < 0.1f)
        {
            return; // don't do tiny rotations.
        }

        Vector3 v = mousePosition - transform.position;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }

        //^^^^^^^^^^ find mouse angle between the mouse and the player. Don't ask how it works. 

        if (angle < 75 && angle > 0 || angle > 285 && angle < 360) //check right angle
        {
            isRight = true;
            isLeft = false;
        }
        else if (angle > 105 && angle < 180 || angle > 180 && angle < 255 ) //check left angle
        {
            isRight = false;
            isLeft = true;
        }
        
    }


    void angleFixing()
    {
        if (angle > 110 && angle < 170 || angle > 290 && angle < 350) //check right angle
        {
            angleFix = 10;
        }
        else if (angle < 70 && angle > 10 || angle > 190 && angle < 250) //check left angle
        {
            angleFix = -10;
        }
        //CORRECTION LERP FOR THE RIGHT SIDE 
        else if (angle < 10)
        {
            angleFix = Mathf.SmoothStep(0, -10, angle / 10);
        }
        else if (angle > 350)
        {
            angleFix = Mathf.SmoothStep(0, 10, (360 - angle) / 10);
        }
        //CORRECTION LERP FOR THE LEFT SIDE
        else if (angle > 170 && angle <= 180)
        {
            angleFix = Mathf.SmoothStep(0, 10, (180 - angle) / 10);
        }
        else if (angle < 190 && angle >= 180)
        {
            angleFix = Mathf.SmoothStep(0, -10, (angle - 180) / 10);
        }
        //CORRECTION LERP FOR THE TOP SIDE
        else if (angle > 70 && angle <= 80)
        {
            angleFix = Mathf.SmoothStep(0, -10, (80 - angle) / 10);
        }
        else if (angle >= 100 && angle < 110)
        {
            angleFix = Mathf.SmoothStep(0, 10, (angle - 100) / 10);
        }
        //CORRECTION LERP FOR THE BOTTOM SIDE
        else if (angle > 250 && angle <= 260)
        {
            angleFix = Mathf.SmoothStep(0, -10, (260 - angle) / 10);
        }
        else if (angle >= 280 && angle < 290)
        {
            angleFix = Mathf.SmoothStep(0, 10, (angle - 280) / 10);
        }
        else
        {
            angleFix = 0;
        }
    }

    void PivotAroundPoint()
    {
        gun.position += (gun.rotation * Pivot);//pivot

        gun.transform.rotation = Quaternion.Euler(0, -angle + angleFix, 0); 
       
        gun.position -= (gun.rotation * Pivot);//pivot

        if (isLeft && !hasMovedL)
        {
            gunRotationGO.transform.rotation = new Quaternion(0, 180, 0,0);
            angle = 270.0f;
            hasMovedL = true;
            hasMovedR = false;
        }
        if (isRight && !hasMovedR)
        {
            gunRotationGO.transform.rotation = new Quaternion(0, 0, 0, 0);
            angle = 60.0f;
            Debug.Log("X moved to : " + gun.position.x);
            hasMovedL = false;
            hasMovedR = true;
        }
    }

 
}
