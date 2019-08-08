using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalBullet : MonoBehaviour
{
    public bool isDestroyable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerTakeDamage(1.0f);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Walls")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "PlayerBullets" && isDestroyable)
        {
            Destroy(this.gameObject);
        }
    }
}
