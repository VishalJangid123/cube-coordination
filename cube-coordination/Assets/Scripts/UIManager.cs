using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private CustomButton restartLevelGameOverButton;
    [SerializeField] private CustomButton homeButton;


    [Header("Level Complete Panel")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject levelCompletePS;
    [SerializeField] private CustomButton restartLevelButton;
    [SerializeField] private CustomButton nextLevelButton;
    [SerializeField] private Text timerDisplayText;
    [SerializeField] private Text bestTimerText;


    [SerializeField] private GameObject extraControlInfoPanel;

    [SerializeField] private CanvasGroup loadingPanelCG;

    [SerializeField] private Text timerText;
    [SerializeField] private Button backToHomeButton;
    [SerializeField] private Button muteButton;

    public Sprite volumeSprite;
    public Sprite volumeMuteSprite;

    float timer = 0;
    bool isRunning = true;
    private float startTime;

    LevelManager levelManager;
    float elapsedTime;

    private void Awake()
    {
        volumeSprite = Resources.Load<Sprite>("volume");
        volumeMuteSprite = Resources.Load<Sprite>("volume-mute");

        levelManager = new LevelManager();
    }

    void Start()
    {
        LoadingOut();
        startTime = Time.time;
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        levelCompletePS.SetActive(false);
        TweenUtility.DelayedAction(this, 5f, () => {
            if (extraControlInfoPanel != null)
            {
                Destroy(extraControlInfoPanel.gameObject);
            }
                });

        

        muteButton.onClick.AddListener(OnVolumeButtonClicked);
        backToHomeButton.onClick.AddListener(OnHomeButtonClicked);
    }

    private void OnHomeButtonClicked()
    {
        LoadScene(0);
    }

    private void OnVolumeButtonClicked()
    {
        GameManager.Instance.IsMuted = !GameManager.Instance.IsMuted;
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime = Time.time - startTime;
            timerText.text = ConvertTimeToReadableString(elapsedTime);
        }
    }

    string ConvertTimeToReadableString(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowGameOverPanel()
    {
        isRunning = false;
        gameOverPanel.SetActive(true);
        CanvasGroup gameOverCanvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
        gameOverCanvasGroup.alpha = 0;
        TweenUtility.DelayedAction(this, 2f, () => {
            StartCoroutine( TweenUtility.FadeCanvasGroup(gameOverCanvasGroup, 1, 1));
        });
        restartLevelGameOverButton.onButtonClick.RemoveAllListeners();
        restartLevelGameOverButton.onButtonClick.AddListener(OnRestartLevelButtonClicked);

        homeButton.onButtonClick.RemoveAllListeners();
        homeButton.onButtonClick.AddListener(OnHomeButtonClicked);
    }

    private void OnRestartLevelButtonClicked()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
        levelCompletePS.SetActive(true);
        CanvasGroup levelCompletePanelCG = levelCompletePanel.GetComponent<CanvasGroup>();
        levelCompletePanelCG.alpha = 0;
        TweenUtility.DelayedAction(this, 0.5f, () => {
            StartCoroutine(TweenUtility.FadeCanvasGroup(levelCompletePanelCG, 1, 1));
        });

        if(SceneManager.sceneCount == SceneManager.GetActiveScene().buildIndex)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            nextLevelButton.onButtonClick.RemoveAllListeners();
            nextLevelButton.onButtonClick.AddListener(OnNextLevelButtonClicked);
        }

        timerDisplayText.text = timerText.text;
        levelManager.SaveBestTime(elapsedTime);


        if(levelManager.GetBestTime() == elapsedTime)
        {
            bestTimerText.text = "New Best!!!";
        }
        else if(levelManager.GetBestTime() != Mathf.Infinity)
        {
            bestTimerText.text = "Best Time = " + ConvertTimeToReadableString(levelManager.GetBestTime());
        }
        else
        {
            bestTimerText.text = "";
        }
    }

    private void OnNextLevelButtonClicked()
    {
        if(SceneManager.sceneCount == SceneManager.GetActiveScene().buildIndex)
        {

        }
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LoadScene(int index)
    {
        LoadingIn();
        TweenUtility.DelayedAction(this, 0.5f, () =>
        {
            SceneManager.LoadScene(index);
        });
    }

    void LoadingIn()
    {
        loadingPanelCG.gameObject.SetActive(true);
        loadingPanelCG.alpha = 0;
        StartCoroutine(TweenUtility.FadeCanvasGroup(loadingPanelCG, 1, 0.3f));
    }

    void LoadingOut()
    {
        loadingPanelCG.gameObject.SetActive(true);
        loadingPanelCG.alpha = 1;
        StartCoroutine(TweenUtility.FadeCanvasGroup(loadingPanelCG, 0, 0.3f));
    }

    public void SetAudioSetting()
    {
        muteButton.image.sprite = GameManager.Instance.IsMuted ? volumeMuteSprite : volumeSprite;
    }
}
