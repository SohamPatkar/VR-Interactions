using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<SFX> soundEffects = new List<SFX>();
    [SerializeField] private AudioSource sfxSource;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(SoundType soundType, bool loop)
    {
        sfxSource.clip = GetSoundClip(soundType);

        if (loop)
        {
            sfxSource.loop = true;
        }
        else
        {
            sfxSource.loop = false;
        }

        sfxSource.Play();
    }

    public void StopAudio()
    {
        sfxSource.Stop();
    }

    private AudioClip GetSoundClip(SoundType soundType)
    {
        SFX sfxObject = soundEffects.Find(x => x.soundType == soundType);

        if (sfxObject == null)
        {
            return null;
        }

        return sfxObject.audioClip;
    }
}

[Serializable]
public class SFX
{
    public AudioClip audioClip;
    public SoundType soundType;
}

public enum SoundType
{
    DESTROY,
    COMPLETED,
    OPENED,
    DRAWERSLIDE,
    GRAB,
    BUTTONCLICK,
    SHOOT
}
