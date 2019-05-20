using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            PlayerManager.Instance.playerTakeDamage();
            Debug.Log("Player got hit");
        }
    }
}
