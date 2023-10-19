using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgSound;
    [SerializeField] private AudioClip playerDieSound;
    [SerializeField] private AudioClip levelCompleteSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = bgSound;
        audioSource.Play();
        audioSource.loop = true;
        print("start");
    }

    public void PlayPlayerDieSound()
    {
        audioSource.loop = false ;
        audioSource.clip = playerDieSound;
        audioSource.Play();
    }

    public void PlayLevelCompleteSound()
    {
        audioSource.loop = false;
        audioSource.clip = levelCompleteSound;
        audioSource.Play();
    }

    public void SetAudioSetting()
    {
        audioSource.mute = GameManager.Instance.VolumeMute;
    }
}
