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
        
    }

    void fireLaser()
    {
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curLaser;
        curLaser = GameObject.Instantiate<GameObject>(playerLaser,gun.position, gun.transform.rotation);
        time = 0.0f;
    }
}
