using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager mInstance = null;
    public static PlayerManager Instance;

    public SpriteRenderer playerSprite;

    public float maxPlayerHealth;
    public float playerHealth;
    public float playerFireRatePerSecond;
    public float playerReflectingBulletFireRate;
    public float playerDashCooldownPerSecond;
    public bool isInvincible = false;
    public bool cutScene = true;
    public float onHitInvincibility = 0.2f;

    bool flashHurt = false;
    float flashTime = 0.1f;
    float curTime = 0.0f;
    float curInvinTime = 0.0f;
    bool hitInvin = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
        disableCollider();
        if (flashHurt)
        playFlashingSprite();
        hitInvincibility();
        if (playerHealth <= 0)
        {
            LoadScenes.Instance.afterGame();
            GameManager.Instance.curState = CurrentGameState.GameLost;
        }

    }

    void disableCollider()
    {
        if (isInvincible)
        GetComponent<BoxCollider>().enabled = false;
        else if (!isInvincible)
        GetComponent<BoxCollider>().enabled = true;
    }

    public void playerTakeDamage(float damage)
    {
        if(!isInvincible)
        {
            SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_PLAYER_HIT);
            displayHUD.Instance.playerGetHit();
            flashHurt = true;
            playerHealth = playerHealth - damage;
            hitInvin = true;
        }
    }

    void playFlashingSprite()
    {
        curTime += Time.deltaTime;

        playerSprite.color = Color.red;

        if (curTime >= flashTime)
        {
            playerSprite.color = Color.white;
            curTime = 0.0f;
            flashHurt = false;
        }
    }

    void hitInvincibility()
    {
        if (hitInvin)
        {
            isInvincible = true;
            curInvinTime += Time.deltaTime;

            if(curInvinTime >= onHitInvincibility)
            {
                isInvincible = false;
                hitInvin = false;
                curInvinTime = 0.0f;
            }
        }
    }
}
