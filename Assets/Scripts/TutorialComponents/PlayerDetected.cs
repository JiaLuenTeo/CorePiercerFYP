using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : MonoBehaviour
{
    public GameObject checkpoint;
    Vector3 checkpointPos;

    void Start()
    {
        checkpointPos = checkpoint.gameObject.transform.position;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.transform.position = new Vector3(checkpointPos.x, collision.gameObject.transform.position.y, checkpointPos.z);

        }
    }
}
