using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootLaser : MonoBehaviour
{

    public static PlayerShootLaser Instance;

    public Transform gunSpawnPosition;
    public Transform gun;
    public GameObject playerLaser;
    public LayerMask collisionMask;
    Vector3 mousePosition;
    LineRenderer lr;

    public float time = 0.0f;
    public float time2 = 0.0f;

    public float maxReflectionCount;
    public float maxStepDistance = 100;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        time = 10.0f;
        time2 = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;

        if (Input.GetMouseButton(0) && time >= PlayerManager.Instance.playerFireRatePerSecond)
        {
            fireLaser();
            SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_PLAYER_SHOOT);
        }
        else if (Input.GetMouseButton(1) && time2 >= PlayerManager.Instance.playerReflectingBulletFireRate)
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
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curRiccochetBullet;
        curRiccochetBullet = GameObject.Instantiate<GameObject>(playerLaser, gun.position, gun.transform.rotation);
        curRiccochetBullet.GetComponent<PlayerReflectBullet>().isLaser = true;
        time = 0.0f;
    }

    void fireRicochet()
    {
        Vector3 newPosition = new Vector3(gun.position.x + mousePosition.x, gun.position.y, gun.position.z + mousePosition.z);
        GameObject curRiccochetBullet;
        curRiccochetBullet = GameObject.Instantiate<GameObject>(playerLaser, gun.position, gun.transform.rotation);
        curRiccochetBullet.GetComponent<PlayerReflectBullet>().isRiccochet = true;
        time2 = 0.0f;
    }
}
