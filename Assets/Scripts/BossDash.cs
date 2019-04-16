using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : MonoBehaviour
{
    public float speed = 2;
    public Transform target;
    Rigidbody rigidbody;
    public float recoverTime;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime);
        transform.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);
        float chargeDistance = distance / 2;

        if (chargeDistance < 4)
        {
            this.transform.GetComponent<BossMove>().enabled = false;
            transform.LookAt(null);
            rigidbody.AddForce(transform.forward, ForceMode.Impulse);

            Debug.Log("GET OVER HERE!!!");
        }
        else
        {
            recoverTime++;

            if(recoverTime == 5)
            {
                this.transform.GetComponent<BossMove>().enabled = true;
                transform.LookAt(target);
            }
            
        }
        Debug.Log(chargeDistance);

        //Debug.Log(distance);


        //Vector3 direction = range ? Vector3.back : Vector3.forward;
        //transform.Translate(direction * Time.deltaTime);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        target = other.transform;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        target = null;
    //    }
    //}
}
