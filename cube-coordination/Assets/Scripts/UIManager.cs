using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;

    [SerializeField] private Button restartLevelButton;
    [SerializeField] private Button nextLevelButton;

    [SerializeField] private Text timerText;

    [SerializeField] private GameObject extraControlInfoPanel;

    [SerializeField] private CanvasGroup loadingPanelCG;

    [SerializeField] private Button backToHomeButton;
    [SerializeField] private Button muteButton;

    public Sprite volumeSprite;
    public Sprite volumeMuteSprite;

    float timer = 0;
    bool isRunning = true;
    private float startTime;

    private void Awake()
    {
        volumeSprite = Resources.Load<Sprite>("volume");
        volumeMuteSprite = Resources.Load<Sprite>("volume-mute");
    }

    void Start()
    {
        LoadingOut();
        startTime = Time.time;
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
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
        GameManager.Instance.VolumeMute = !GameManager.Instance.VolumeMute;
    }

    private void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
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
        restartLevelButton.onClick.RemoveAllListeners();
        restartLevelButton.onClick.AddListener(OnRestartLevelButtonClicked);
    }

    private void OnRestartLevelButtonClicked()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
        CanvasGroup levelCompletePanelCG = levelCompletePanel.GetComponent<CanvasGroup>();
        levelCompletePanelCG.alpha = 0;
        TweenUtility.DelayedAction(this, 0.5f, () => {
            StartCoroutine(TweenUtility.FadeCanvasGroup(levelCompletePanelCG, 1, 1));
        });
        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
    }

    private void OnNextLevelButtonClicked()
    {
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
        muteButton.image.sprite = GameManager.Instance.VolumeMute ? volumeMuteSprite : volumeSprite;
    }
}
