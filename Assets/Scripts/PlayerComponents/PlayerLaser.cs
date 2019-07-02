using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerReflectBullet : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 getFlyingPos;
    Vector3 lastFrameVelocity;
    Rigidbody rb;
    Quaternion getRotation;
    public float bulletSpeed = 10.0f;
    public bool isLaser, isRiccochet;
    public float bulletDamage = 1.0f;
    public LayerMask collisionMask;
    float killTime = 2.0f;
    float time;
    


    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        getFlyingPos = new Vector3(mousePosition.x, 0.0f, mousePosition.z).normalized;
        rb = GetComponent<Rigidbody>();
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
        rb.velocity = transform.right * bulletSpeed;


        time += Time.deltaTime;

        if (time >= killTime)
            GameObject.Destroy(this.gameObject);
    }

    void riccochetBounceFly()
    {

        rb.velocity = transform.right * bulletSpeed;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.right);

        if (Physics.Raycast(ray, out hit, Time.deltaTime * bulletSpeed + .1f, collisionMask))
        {
            Vector3 reflection = Vector3.Reflect(ray.direction, hit.normal);
            rb.velocity = reflection * bulletSpeed;
            Quaternion rotation = Quaternion.FromToRotation(lastFrameVelocity, reflection);
            transform.rotation = rotation * transform.rotation;
        }

        time += Time.deltaTime;

        if (time >= killTime)
            GameObject.Destroy(this.gameObject);

    }

    private void FixedUpdate()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            BossAI.Instance.bossHealth -= bulletDamage;
            GameObject.Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Walls" && isLaser == true)
        {
            GameObject.Destroy(this.gameObject);
        }

    }


}


