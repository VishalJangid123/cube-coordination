using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup normal;
    [SerializeField] private CanvasGroup hover;

    public UnityEvent onButtonClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onButtonClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(TweenUtility.FadeCanvasGroup(hover, 1, 0.3f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(TweenUtility.FadeCanvasGroup(hover, 0, 0.3f));
    }
}
