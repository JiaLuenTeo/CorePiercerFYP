using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 getFlyingPos;
    Quaternion getRotation;
    public float bulletSpeed = 10.0f;
    public bool isLaser, isRiccochet;
    float killTime = 2.0f;
    float time;
    public LayerMask collisionMask;


    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        getFlyingPos = new Vector3(mousePosition.x, 0.0f, mousePosition.z).normalized;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaser)
        {
            laserFly();
        }
        else if (isRiccochet)
        {
            riccochetBounceFly();
        }
       

    }

    void laserFly()
    {
        this.transform.Translate(getFlyingPos * Time.deltaTime * bulletSpeed);


        time += Time.deltaTime;

        if (time >= killTime)
            GameObject.Destroy(this.gameObject);
    }

    void riccochetBounceFly()
    {

        this.transform.Translate(getFlyingPos * Time.deltaTime * bulletSpeed);

        Ray ray = new Ray(this.transform.position, transform.forward);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit, Time.deltaTime * bulletSpeed + .1f,collisionMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }

       


    }

    
}
