using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AudioClipID
{
	BGM_MAIN_MENU = 0,
	BGM_GAMEPLAY = 1,
	BGM_WHEEL = 2,
	BGM_LOSE = 3,
	BGM_WIN = 4,


	SFX_PLAYER_SHOOT = 103,
    SFX_PLAYER_DODGE = 104,
    SFX_PLAYER_HIT = 105,
    SFX_PLAYER_DEATH = 106,
    SFX_BOSS_SHOOT = 107,
    SFX_BOSS_SCATTER = 108,
    SFX_BOSS_HIT = 109,
    SFX_BOSS_DEATH = 110,


	TOTAL = 9001
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID audioClipID;
	public AudioClip audioClip;
}

public class SoundManagerScript : MonoBehaviour 
{
	private static SoundManagerScript mInstance = null;

    public static SoundManagerScript Instance;
	

	public float bgmVolume = 1.0f;
	public float sfxVolume = 1.0f;

	
	public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo>();
	
	public AudioSource bgmAudioSource;
	public AudioSource sfxAudioSource;
	
	public List<AudioSource> sfxAudioSourceList = new List<AudioSource>();
	public List<AudioSource> bgmAudioSourceList = new List<AudioSource>();

	// Preload before any Start() runs in other scripts
	void Awake () 
	{
        if (Instance == null)
            Instance = this;
        else
            Object.Destroy(gameObject);

        AudioSource[] audioSourceList = this.GetComponentsInChildren<AudioSource>();

        if (audioSourceList[0].gameObject.name == "BGMAudioSource")
        {
            bgmAudioSource = audioSourceList[0];
            sfxAudioSource = audioSourceList[1];
        }
        else
        {
            bgmAudioSource = audioSourceList[1];
            sfxAudioSource = audioSourceList[0];
        }
    }

	// Use this for initialization
	void Start () 
	{
		//PlayBGM(AudioClipID.BGM_GAMEPLAY);

	}
	
	// Update is called once per frame
	void Update () 
	{
        if(!bgmAudioSource.isPlaying)
        {
            PlayBGM(AudioClipID.BGM_GAMEPLAY);
            Debug.Log("IS PLAYING BGM");
        }
        

        bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        bgmAudioSource.volume = bgmVolume;

        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        sfxAudioSource.volume = sfxVolume;
    }
	
	AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for(int i=0; i<audioClipInfoList.Count; i++)
		{
			if(audioClipInfoList[i].audioClipID == audioClipID)
			{
				return audioClipInfoList[i].audioClip;
			}
		}

		Debug.LogError("Cannot Find Audio Clip : " + audioClipID);

		return null;
	}
	
	//! BACKGROUND MUSIC (BGM)
	public void PlayBGM(AudioClipID audioClipID)
	{
		bgmAudioSource.clip = FindAudioClip(audioClipID);
		Debug.Log (audioClipID);
		bgmAudioSource.volume = bgmVolume;
		bgmAudioSource.loop = true;
		bgmAudioSource.Play();
	}
	
	public void PauseBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Pause();
		}
	}
	
	public void StopBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Stop();
		}
	}
	
	
	//! SOUND EFFECTS (SFX)
	public void PlaySFX(AudioClipID audioClipID)
	{
		sfxAudioSource.PlayOneShot(FindAudioClip(audioClipID), sfxVolume);
	}
	
	public void PlayLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);
		
		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPlay)
			{
				if(sfxAudioSourceList[i].isPlaying)
				{
					return;
				}

				sfxAudioSourceList[i].volume = sfxVolume;
				sfxAudioSourceList[i].Play();
				return;
			}
		}
		
		AudioSource newInstance = gameObject.AddComponent<AudioSource>();
		newInstance.clip = clipToPlay;
		newInstance.volume = sfxVolume;
		newInstance.loop = true;
		newInstance.Play();
		sfxAudioSourceList.Add(newInstance);
	}
	
	public void PauseLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip(audioClipID);
		
		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToPause)
			{
				sfxAudioSourceList[i].Pause();
				return;
			}
		}
	}	
	
	public void StopLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);
		
		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToStop)
			{
				sfxAudioSourceList[i].Stop();
				return;
			}
		}
	}

	public void ChangePitchLoopingSFX(AudioClipID audioClipID, float value)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		for(int i=0; i<sfxAudioSourceList.Count; i++)
		{
			if(sfxAudioSourceList[i].clip == clipToStop)
			{
				sfxAudioSourceList[i].pitch = value;
				return;
			}
		}
	}
	
	public void SetBGMVolume(float value)
	{
		bgmVolume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
	
	public void SetSFXVolume(float value)
	{
		sfxVolume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}