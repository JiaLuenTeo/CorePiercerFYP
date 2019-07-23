using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHUD : MonoBehaviour
{
    public static displayHUD Instance;

    float flashTime = 0.5f;
    public float curTime = 0.0f;

    public Image playerHealth;
    public Image bossHealth;
    public Image dashAbilityCD, bounceAbilityCD;
    public GameObject playerHit;
    public Text timer;

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
        

        healthFill();
        dashAbilityFill();
        bouncingAbilityFill();
        potraitFlash();
        timerCount();
        

    }

    void healthFill()
    {
        playerHealth.fillAmount = PlayerManager.Instance.playerHealth / PlayerManager.Instance.maxPlayerHealth;
        bossHealth.fillAmount = BossAI.Instance.bossHealth / BossAI.Instance.maxbossHealth;
    }

    public void playerGetHit()
    {
        playerHit.gameObject.SetActive(true);
        curTime = 0.0f;
    }

    public void dashAbilityFill()
    {
        dashAbilityCD.fillAmount = PlayerMovement.Instance.dashCurTime / PlayerMovement.Instance.dashTime;
    }

    public void bouncingAbilityFill()
    {
        bounceAbilityCD.fillAmount = PlayerShootLaser.Instance.time2 / PlayerManager.Instance.playerReflectingBulletFireRate;
    }

    void potraitFlash()
    {
        curTime += Time.deltaTime;

        if (curTime >= flashTime)
        {
            playerHit.gameObject.SetActive(false);
        }
    }

    void timerCount()
    {
        timer.text = "Time : " + GameManager.Instance.curGameTime.ToString("F2");
    }
}
