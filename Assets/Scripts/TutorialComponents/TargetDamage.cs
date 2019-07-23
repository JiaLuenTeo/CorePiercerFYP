using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    private GameObject[] targets;
    private int health = 1; 
    public GameObject wall;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);

        //MoveWall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int damage = 1;
        if(collision.gameObject.tag == "PlayerBullets")
        {
            health -= damage;
        }
    }

    void MoveWall()
    {
        Vector3 newPos = new Vector3(0f, 0f, -7.5f);
        wall.transform.position = newPos;
    }
}
