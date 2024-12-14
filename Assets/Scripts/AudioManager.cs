using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioClip _metamorhposeBegin, _metamorphoseEnd;
    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void PlaySound(AudioClip sound)
    {
        _sfx.PlayOneShot(sound);
    }

    public void PlayMetamorphoseBeginSound()
    {
        _sfx.PlayOneShot(_metamorhposeBegin);
    }
    
    public void PlayMetamorphoseEndSound()
    {
        _sfx.PlayOneShot(_metamorphoseEnd);
    }
}
