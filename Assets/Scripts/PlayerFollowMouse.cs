using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{
    public Vector3 Pivot;
    public bool DebugInfo = true;

    public Transform gun;
    public Transform offsetR, offsetL;
    Vector3 mousePosition;
    public float angle;
    public bool isRight = true;
    public bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        FindDistance();
    }

    // Update is called once per frame
    void Update()
    {

        //gun.position = Vector3.Lerp(gun.position, mousePosition, speed);
        //gun.RotateAround(playerCharacter.position, gun.forward, angle );

        /* Vector3 orbVector = Camera.main.WorldToScreenPoint(playerCharacter.position);
         orbVector = Input.mousePosition - orbVector;
         float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;
         Debug.Log(angle);

         gun.position = playerCharacter.position + new Vector3(0.2f,0);
         gun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
        findMouseAngle();
        PivotAroundPoint();
    }

    void FindDistance()
    {
        if(isRight)
        Pivot.x = offsetR.position.x - gun.position.x;
        else if (isLeft)
        Pivot.x = offsetL.position.x - gun.position.x;

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

        if (angle < 80 && angle > 0 || angle > 280 && angle < 360)
        {
            isRight = true;
            isLeft = false;
        }
        else if (angle > 100 && angle < 180 || angle > 180 && angle < 260 )
        {
            isRight = false;
            isLeft = true;
        }
    }

    void PivotAroundPoint()
    {
        gun.position += (gun.rotation * Pivot);

        gun.transform.rotation = Quaternion.Euler(0,-angle,0);

        gun.position -= (gun.rotation * Pivot);

        if (isRight)
        {
            //gun.position = new Vector3(0.64f, gun.position.y, gun.position.z);
           // FindDistance();
        }
        else if (isLeft)
        {

           gun.position = new Vector3(-gun.position.x, gun.position.y, gun.position.z);
           // FindDistance();
        }

        if (DebugInfo)
        {
            Debug.DrawRay(gun.position, gun.rotation * Vector3.up, Color.black);
            Debug.DrawRay(gun.position, gun.rotation * Vector3.right, Color.black);
            Debug.DrawRay(gun.position, gun.rotation * Vector3.forward, Color.black);

            Debug.DrawRay(gun.position + (gun.rotation * Pivot), gun.rotation * Vector3.up, Color.green);
            Debug.DrawRay(gun.position + (gun.rotation * Pivot), gun.rotation * Vector3.right, Color.red);
            Debug.DrawRay(gun.position + (gun.rotation * Pivot), gun.rotation * Vector3.forward, Color.blue);
        }
    }
}
