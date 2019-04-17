using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 getFlyingPos;
    Quaternion getRotation;
    public float bulletSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        getFlyingPos = new Vector3(mousePosition.x, 0.0f, mousePosition.z).normalized;
       
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate( getFlyingPos * Time.deltaTime * bulletSpeed);
        

    }
}
