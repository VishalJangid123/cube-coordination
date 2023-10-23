using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Start Menu Panel")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private CustomButton startButton;
    [SerializeField] private CustomButton howToPlayButton;
    [SerializeField] private CustomButton creditsButton;
    [SerializeField] private Button volumeButton;
    [SerializeField] private Image volumeButtonImage;

    [Header("Loading")]
    [SerializeField] private CanvasGroup loadingPanelCG;

    [Header("Level Panel")]
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private Transform levelButtonContentParent;
    [SerializeField] private GameObject levelButtonPrefab;


    [Header("How To play")]
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private CustomButton backToHomeButton;
   

    int currentLevel = 0;

    public Sprite volumeSprite;
    public Sprite volumeMuteSprite;


    public bool isMuted; // 0 = unmute; 1= mute
    public bool IsMuted
    {
        get { return PlayerPrefs.GetInt(Env.VOLUME_MUTE) == 1; }
        set
        {
            PlayerPrefs.SetInt(Env.VOLUME_MUTE, value ? 1 : 0);
            UpdateVolumeSetting();
        }
    }

    private void UpdateVolumeSetting()
    {
        volumeButtonImage.sprite = IsMuted ? volumeMuteSprite : volumeSprite;
    }

    private void Awake()
    {
        volumeSprite = Resources.Load<Sprite>("volume");
        volumeMuteSprite = Resources.Load<Sprite>("volume-mute");
    }

    private void Start()
    {

        if (PlayerPrefs.HasKey(Env.CURRENT_LEVEL_PREFS))
        {
            currentLevel = PlayerPrefs.GetInt(Env.CURRENT_LEVEL_PREFS);
        }
        else
        {
            currentLevel = 1;
        }

        levelPanel.SetActive(false);

        startButton.onButtonClick.AddListener(() => { ShowLevelPanel(); });
        volumeButton.onClick.AddListener(OnVolumeButtonClicked);

        howToPlayButton.onButtonClick.RemoveAllListeners();
        howToPlayButton.onButtonClick.AddListener(() =>
        {
            howToPlayPanel.SetActive(true);
        });

        backToHomeButton.onButtonClick.RemoveAllListeners();
        backToHomeButton.onButtonClick.AddListener(() =>
        {
            howToPlayPanel.SetActive(false);
        });

        howToPlayPanel.SetActive(false);
}

    private void OnVolumeButtonClicked()
    {
        IsMuted = !IsMuted;
    }

    void ShowLevelPanel()
    {
        LoadingIn();
        LoadingOut();

        levelPanel.SetActive(true);

        int levels = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;
        for(int level=1; level<= levels; level++)
        {
            GameObject btn = Instantiate(levelButtonPrefab, levelButtonContentParent);
            btn.GetComponentInChildren<Text>().text = level.ToString();
            if(level == currentLevel)
            {
                btn.GetComponent<Image>().color = Color.red;
            }
            else if(level > currentLevel)
            {
                btn.GetComponent<Button>().interactable = false;
            }

            if(level <= currentLevel)
            {
                int levelCopy = level;
                btn.GetComponent<Button>().onClick.AddListener(delegate {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(levelCopy);
                    LoadingIn();
                    LoadingOut();
                });
            }
        }
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
}
