using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spiderMinesTrack : MonoBehaviour
{
    public float mineHealth = 2.0f;
    Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.position);

        if (mineHealth <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerTakeDamage(2.0f);
            GameObject.Destroy(this.gameObject);
        }
    }
}
