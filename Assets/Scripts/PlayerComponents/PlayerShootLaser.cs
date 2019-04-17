using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{
    public Transform gunSpawnPosition;
    public Transform gun;
    public GameObject playerLaser;
    public float timePerShot;
    Vector3 mousePosition;
    public float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetMouseButton(0) && time >= timePerShot)
        {
            fireLaser();
           
        }
        
    }

    void fireLaser()
    {
        GameObject curLaser;
        curLaser = GameObject.Instantiate<GameObject>(playerLaser,gun.position, gun.transform.rotation);
        time = 0.0f;
    }
}
