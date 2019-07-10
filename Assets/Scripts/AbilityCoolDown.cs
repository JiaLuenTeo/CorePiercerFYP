using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour
{
    public Image abilityCD;
    public float cooldown = 5;
    public bool ready;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ready = true;
        }

        if (ready)
        {
            abilityCD.fillAmount = 0;


            ready = false;
        }

        if (!ready)
        {
            abilityCD.fillAmount += 1 / PlayerManager.Instance.playerDashCooldownPerSecond * Time.deltaTime;
        }

    }
}
