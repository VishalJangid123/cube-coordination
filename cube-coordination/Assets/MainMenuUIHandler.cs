using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [Header("Start Menu Panel")]
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private CustomButton startButton;

    [Header("Loading")]
    [SerializeField] private CanvasGroup loadingPanelCG;

    [Header("Level Panel")]
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private Transform levelButtonContentParent;
    [SerializeField] private GameObject levelButtonPrefab;

    readonly string CURRENT_LEVEL_PREFS = "currentlevel";
    readonly string LEVEL_TIMER_PREFS = "level_";

    int currentLevel = 0;

    private void Start()
    {

        if (PlayerPrefs.HasKey(CURRENT_LEVEL_PREFS))
        {
            currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_PREFS);
        }
        else
        {
            currentLevel = 1;
        }

        levelPanel.SetActive(false);

        startButton.onButtonClick.AddListener(() => { ShowLevelPanel(); });
    }

    void ShowLevelPanel()
    {
        LoadingIn();
        LoadingOut();

        levelPanel.SetActive(true);

        int levels = 4;
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
