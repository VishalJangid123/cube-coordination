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

    

    public bool isMuted; // 0 = unmute; 1= mute
    public bool IsMuted {
        get { return PlayerPrefs.GetInt(Env.VOLUME_MUTE, 0) == 1; }
        set {
            
            PlayerPrefs.SetInt(Env.VOLUME_MUTE, value ? 1 : 0);
            UpdateVolumeSetting();
        }
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
    }

    private void Start()
    {
        
        UpdateVolumeSetting();
    }

    private void UpdateVolumeSetting()
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
        PlayerPrefs.SetInt(Env.CURRENT_LEVEL_PREFS, SceneManager.GetActiveScene().buildIndex);

        AudioManager.PlayLevelCompleteSound();
        UIManager.ShowLevelCompletePanel();
    }

}
