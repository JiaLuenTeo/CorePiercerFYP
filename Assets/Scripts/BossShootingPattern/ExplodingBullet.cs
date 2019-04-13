using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{

    public int numberOfBullet;
    public float bulletSpeed;
    public GameObject BulletPrefab;
 

    private Vector3 startPoint;
    private const float radius = 1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            startPoint = transform.position;
            shoot();
        }
    }

    public void shoot()
    {
        Vector3 bulletMoveDirection = startPoint.normalized * bulletSpeed;

        GameObject bullet = Instantiate(BulletPrefab, startPoint, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);
        Destroy(bullet, 3.1F);
    }

    


}


