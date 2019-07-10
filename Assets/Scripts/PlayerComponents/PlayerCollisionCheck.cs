using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    bool hasLeaveCollision = true;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && hasLeaveCollision == true)
        {
            PlayerManager.Instance.playerTakeDamage(1.0f);
            Debug.Log("Player got hit");
            hasLeaveCollision = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet" && hasLeaveCollision == false)
        {
            hasLeaveCollision = true;
        }
    }
}
