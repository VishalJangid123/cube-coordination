using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager 
{
    private int levelNumber = SceneManager.GetActiveScene().buildIndex;
    private float bestTime = Mathf.Infinity;

    public LevelManager()
    {
        LoadBestTime();
    }

    public void SaveBestTime(float newTime)
    {
        LoadBestTime();
        if (newTime < bestTime)
        {
            bestTime = newTime;
            PlayerPrefs.SetFloat($"BestTime_Level_{levelNumber}", bestTime);
            PlayerPrefs.Save();
        }
    }

    public float GetBestTime()
    {
        return bestTime;
    }

    private void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat($"BestTime_Level_{levelNumber}", Mathf.Infinity);
    }


}
