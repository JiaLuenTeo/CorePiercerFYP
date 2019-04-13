using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{
    public Vector3 Pivot;
    public bool DebugInfo = true;

    public Transform gun;
    public Transform offset;
    public float speed = 10.0f;
    Vector3 mousePosition;
    public float angle;
    public bool hasRotated = false;

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
        Pivot.x = offset.position.x - gun.position.x;
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
    }

    void PivotAroundPoint()
    {
        

        gun.position += (gun.rotation * Pivot);

        if (gun.eulerAngles.y >= 90 && hasRotated == false)
        {
            Vector3 rotationAngle = new Vector3(0, 270, 0);
            gun.rotation = Quaternion.Euler(rotationAngle);
            hasRotated = true;
        }
        else gun.transform.rotation = Quaternion.Euler(0,-angle,0);
        //gun.rotation *= Quaternion.AngleAxis(45* angle, Vector3.up);
        
        if (hasRotated == true && gun.eulerAngles.y <= 10)
        {
            hasRotated = false;
        }

        gun.position -= (gun.rotation * Pivot);

        

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
