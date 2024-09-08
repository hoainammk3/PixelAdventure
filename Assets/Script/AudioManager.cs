using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _playerJumpClip;
    [SerializeField] private AudioClip _claimClip;
    [SerializeField] private AudioClip _die;
    [SerializeField] private AudioClip _onHit;
    [SerializeField] private AudioClip _enermyShot;
    
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.AddComponent<AudioManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null) Destroy(_instance.gameObject);
    }

    public void PlayClickCip()
    {
        _sfxSource.PlayOneShot(_clickClip);
    }

    public void PlayPlayerJumpClip()
    {
        _sfxSource.PlayOneShot(_playerJumpClip);
    }

    public void PlayClaimClip()
    {
        _sfxSource.PlayOneShot(_claimClip);
    }

    public void PlayDieClip()
    {
        _sfxSource.PlayOneShot(_die);
    }

    public void PlayOnHitClip()
    {
        _sfxSource.PlayOneShot(_onHit);
    }

    public void PlayEnermyShotClip()
    {
        _sfxSource.PlayOneShot(_enermyShot);
    }

    public void PlayMusic()
    {
        _musicSource.volume = 1;
        _musicSource.Play();
    }

    public void OffMusic()
    {
        _musicSource.volume = 0;
    }
    
    public void OffSfx()
    {
        _sfxSource.volume = 0;
    }

    public void OnSfx()
    {
        _sfxSource.volume = 1;
    }
}
