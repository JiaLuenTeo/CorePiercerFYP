using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{
    public Transform gun;
    private LineRenderer lr;
    Vector3 mousePosition;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        fireLaser();
    }

    void fireLaser()
    {
        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 lookDir = mousePosition - transform.position;

        lr.SetPosition(0, gun.position);
        RaycastHit hit;
        if (Physics.Raycast(gun.position, gun.transform.position, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, gun.transform.right * 5000);
    }
}
