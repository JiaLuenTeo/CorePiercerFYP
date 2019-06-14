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
        if (Input.GetKeyDown(KeyCode.A))
        {
            ready = true;
        }

        if (ready)
        {
            abilityCD.fillAmount += 1 / cooldown * Time.deltaTime;

            if(abilityCD.fillAmount >= 1)
            {
                abilityCD.fillAmount = 0;
                ready = false;
            }
        }
    }
}
