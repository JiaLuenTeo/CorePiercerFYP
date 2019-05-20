using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager mInstance = null;
    public static PlayerManager Instance;

    public float playerHealth;
    public float playerFireRatePerSecond;
    public float playerDashCooldownPerSecond;
    public bool isInvincible = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerTakeDamage()
    {
        if(!isInvincible)
        {
            playerHealth = playerHealth - 1;
        }
    }
}
