using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{
    public Transform gunSpawnPosition;
    public Transform gun;
    public GameObject playerLaser;
    Vector3 mousePosition;
    public float time;

    public float maxReflectionCount;
    public float maxStepDistance = 100;

    // Use this for initialization
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetMouseButton(0) && time >= PlayerManager.Instance.playerFireRatePerSecond)
        {
            fireLaser();
        }
        else if (Input.GetMouseButton(1) && time >= PlayerManager.Instance.playerFireRatePerSecond)
        {
            fireRicochet();
        }
        
    }

    void fireLaser()
    {
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curLaser;
        curLaser = GameObject.Instantiate<GameObject>(playerLaser,gun.position, gun.transform.rotation);
        curLaser.GetComponent<PlayerLaser>().isLaser = true;
        time = 0.0f;
    }

    void fireRicochet()
    {
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curRiccochetBullet;
        curRiccochetBullet = GameObject.Instantiate<GameObject>(playerLaser, gun.position, gun.transform.rotation);
        curRiccochetBullet.GetComponent<PlayerLaser>().isRiccochet = true;
        time = 0.0f;
    }
}
