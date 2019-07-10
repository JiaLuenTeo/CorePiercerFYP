using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplodingBullet : MonoBehaviour
{

    private const float radius = 1f;
    public float numOfBullets = 20.0f;
    public float bulletSpeed = 5.0f;
    public float timeToExplode = 3.0f;
    public GameObject normalBulletPrefab;
    float curTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;


        if (curTime >= timeToExplode)
        {
            spawnRadialBullet(0, 360 / numOfBullets,this.transform.position);
            GameObject.Destroy(this.gameObject);
        }
    }

    void spawnRadialBullet(float angle, float anglestep, Vector3 hitPosition)
    {
        for (int i = 0; i < numOfBullets ; ++i)
        {
            float projectileX = hitPosition.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileZ = hitPosition.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileX, projectileZ, 0.0f);
            Vector3 bulletMoveDirection = (projectileVector - hitPosition);

            GameObject tmpObj = Instantiate(normalBulletPrefab, hitPosition, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y).normalized * bulletSpeed;

            angle += anglestep;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerTakeDamage(1.0f);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Exploding")
        {
            spawnRadialBullet(0, 360 / numOfBullets, this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
