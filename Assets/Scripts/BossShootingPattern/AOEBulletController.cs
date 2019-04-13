using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBulletController : MonoBehaviour {


    public int numberOfBullet;
    public float bulletSpeed;
    public GameObject BulletPrefab;

    private Vector3 startPoint;
    private const float radius = 1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = transform.position;
            SpawnBullet(numberOfBullet);
        }
    }

    

    private void SpawnBullet(int _numberOfBullets)
    {
        float angleStep = 180f / _numberOfBullets;
        float angle = 0f;

        for (int i = 0; i <= _numberOfBullets - 1; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

            GameObject tmpObj = Instantiate(BulletPrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);

            Destroy(tmpObj, 10F);

            angle += angleStep;
        }
    }
}