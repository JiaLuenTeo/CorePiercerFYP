using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public float delayExplode;
    public int numberOfMiniBullet;
    public float miniBulletSpeed;
    public GameObject miniBulletPrefab;


    private Vector3 startPoint;
    private const float radius = 1f;

    // Use this for initialization
    void Start () {
        StartCoroutine(Exploding());
	}
	
	// Update is called once per frame
	void Update () {
        startPoint = transform.position;
    }

    IEnumerator Exploding()
    {
        yield return new WaitForSeconds(delayExplode);

        float angleStep = 360f / numberOfMiniBullet;
        float angle = 0f;

        for (int i = 0; i <= numberOfMiniBullet - 1; i++)
        {
            float bulletDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float bulletDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
            Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * miniBulletSpeed;

            GameObject tmpObj = Instantiate(miniBulletPrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(bulletMoveDirection.x, 0, bulletMoveDirection.y);

            Destroy(tmpObj, 10F);

            angle += angleStep;
        }
    }
}
