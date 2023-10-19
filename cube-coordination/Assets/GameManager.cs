using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public UIManager UIManager { get; private set; }

    public bool gameOver = false;

    readonly string CURRENT_LEVEL_PREFS = "currentlevel";
    readonly string LEVEL_TIMER_PREFS = "level_";
    readonly string VOLUME_MUTE = "volume_mute";

    public int volumeMute; // 0 = unmute; 1= mute
    public bool VolumeMute {
        get { return volumeMute == 1 ? true : false; }
        set {
            volumeMute = value == true ? 1 : 0;
            PlayerPrefs.SetInt(VOLUME_MUTE, volumeMute); SetVolumeSetting();  }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        AudioManager = GetComponent<AudioManager>();
        UIManager = GetComponent<UIManager>();

        if (PlayerPrefs.HasKey(VOLUME_MUTE))
        {
            volumeMute = PlayerPrefs.GetInt(VOLUME_MUTE);
        }
        else
        {
            volumeMute = 0;
        }

    }

    private void Start()
    {
        
        SetVolumeSetting();
    }

    private void SetVolumeSetting()
    {
        UIManager.SetAudioSetting();
        AudioManager.SetAudioSetting();
    }

    public void GameOver()
    {
        AudioManager.PlayPlayerDieSound();
        UIManager.ShowGameOverPanel();
        gameOver = true;
    }

    public void LevelComplete()
    {
        gameOver = true;
        PlayerPrefs.SetInt(CURRENT_LEVEL_PREFS, SceneManager.GetActiveScene().buildIndex);

        AudioManager.PlayLevelCompleteSound();
        UIManager.ShowLevelCompletePanel();
    }

}
