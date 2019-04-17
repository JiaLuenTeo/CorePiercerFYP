using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{
    public Vector3 Pivot;
    public bool DebugInfo = true;

    public Transform gun;
    public Transform gunRotationGO;
    public Transform offsetR, offsetL;
    public Transform handR, handL;

    Vector3 mousePosition;
    float angle;

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

    void PivotAroundPoint()
    {
        gun.position += (gun.rotation * Pivot);//pivot

        gun.transform.rotation = Quaternion.Euler(0,-angle,0); //rotate gun towards mouse

        gun.position -= (gun.rotation * Pivot);//pivot

        if (isLeft && !hasMovedL)
        {
            gunRotationGO.transform.rotation = new Quaternion(0, 180, 0,0);
            hasMovedL = true;
            hasMovedR = false;
        }
        if (isRight && !hasMovedR)
        {
            gunRotationGO.transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("X moved to : " + gun.position.x);
            hasMovedL = false;
            hasMovedR = true;
        }
    }

 
}
