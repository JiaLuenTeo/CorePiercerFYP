using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsSearch : MonoBehaviour
{

    public Text sfxVolume, bgmVolume;
    public Slider sfxSlider, bgmSlider;

    float bgmNum, sfxNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgmNum = PlayerPrefs.GetFloat("BGMVolume");
        sfxNum = PlayerPrefs.GetFloat("SFXVolume");

        sfxVolume.text = sfxNum.ToString("F2");
        bgmVolume.text = bgmNum.ToString("F2");

        sfxSlider.value = sfxNum;
        bgmSlider.value = bgmNum;
    }
}
