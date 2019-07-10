using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHUD : MonoBehaviour
{
    public Image playerHealth;
    public Image bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.fillAmount = PlayerManager.Instance.playerHealth / 10;
        bossHealth.fillAmount = BossAI.Instance.bossHealth / 100;
    }
}
