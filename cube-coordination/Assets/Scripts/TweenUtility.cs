using System;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class TweenUtility
{
    public static void DelayedAction(MonoBehaviour monoBehaviour, float delay, System.Action action)
    {
        monoBehaviour.StartCoroutine(DelayedActionCoroutine(delay, action));
    }

    public static IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        if (canvasGroup == null)
        {
            UnityEngine.Debug.LogWarning("CanvasGroup is null.");
            yield return null;
        }

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            if (elapsedTime >= duration)
            {
                canvasGroup.alpha = targetAlpha;
                canvasGroup.interactable = targetAlpha > 0;
                canvasGroup.blocksRaycasts = targetAlpha > 0;
            }
            yield return null;
        }
    }


    private static IEnumerator DelayedActionCoroutine(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
