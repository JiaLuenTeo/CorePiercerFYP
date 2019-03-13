using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowMouse : MonoBehaviour
{
    public Transform gun;
    public Transform playerCharacter;
    public float speed = 10.0f;
    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        //gun.position = Vector3.Lerp(gun.position, mousePosition, speed);
        //gun.RotateAround(playerCharacter.position, gun.forward, angle );

        Vector3 orbVector = Camera.main.WorldToScreenPoint(playerCharacter.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;
        Debug.Log(angle);

        gun.position = playerCharacter.position + new Vector3(0.2f,0);
        gun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
