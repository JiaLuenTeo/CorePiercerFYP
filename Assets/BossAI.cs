using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public enum BossCurrentState
    {
        IDLE,
        MOVING,
        SHOOTINGNORMAL1,
        SHOOTINGEXPLODING,
        SHOOTINGLASER,
        CHARGING,
        HURT,
        TOTAL
    }

    BossMove bossMovement;
    AOEBulletController bossBulletMovement;
    ExplodingBullet bossExplodingBullet;
    public BossCurrentState curState, preState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
