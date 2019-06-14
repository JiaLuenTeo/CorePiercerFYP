using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{
    public Transform gunSpawnPosition;
    public Transform gun;
    public GameObject playerLaser;
    public LayerMask collisionMask;
    Vector3 mousePosition;
    LineRenderer lr;

    public float time;

    public float maxReflectionCount;
    public float maxStepDistance = 100;

    // Use this for initialization
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetMouseButton(0) && time >= PlayerManager.Instance.playerFireRatePerSecond)
        {
            fireLaser();
        }
        else if (Input.GetMouseButton(1) && time >= PlayerManager.Instance.playerReflectingBulletFireRate)
        {
            fireRicochet();
        }
        else if (time >= 0.5f)
        {
            lr.enabled = false;
        }
       
    }

    void fireLaser()
    {
        lr.enabled = true;
        lr.SetPosition(0, gun.position);
        lr.startWidth = 0.05f;

        Ray ray = new Ray(gun.position, gun.transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider)
            { 
                lr.SetPosition(1, hit.point);
            }
        }
        time = 0.0f;
    }

    void fireRicochet()
    {
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curRiccochetBullet;
        curRiccochetBullet = GameObject.Instantiate<GameObject>(playerLaser, gun.position, gun.transform.rotation);
        curRiccochetBullet.GetComponent<PlayerReflectBullet>().isRiccochet = true;
        time = 0.0f;
    }
}
