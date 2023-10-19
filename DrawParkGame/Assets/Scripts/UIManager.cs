using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] LinesDrawer linesDrawer;

    [Space]
    [SerializeField] private CanvasGroup availableLineCanvasGroup;
    [SerializeField] private GameObject avaliableLineHolder;
    [SerializeField] private Image avaliableLineFill;
    private bool isAvaliableLineUIActive = false;

    [Space]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    private Route activeRoute;

    private void Start()
    {
        fadePanel.DOFade(0f,fadeDuration).From(1f);

        availableLineCanvasGroup.alpha = 0f;

        linesDrawer.OnBeginDraw += OnBeginDrawHandler;
        linesDrawer.OnDraw += OnDrawHandler;
        linesDrawer.OnEndDraw += OnEndDrawHandler;
    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;

        avaliableLineFill.color = activeRoute.carColor;
        avaliableLineFill.fillAmount = 1f;
        availableLineCanvasGroup.DOFade(1f,.3f).From(0f);
        isAvaliableLineUIActive = true;
    }

    private void OnDrawHandler()
    {
        if(isAvaliableLineUIActive)
        {
            float maxLineLength = activeRoute.maxLineLength;
            float lineLength = activeRoute.line.length;

            avaliableLineFill.fillAmount = 1 - (lineLength/maxLineLength);
        }   
    }

    private void OnEndDrawHandler()
    {   
        if(isAvaliableLineUIActive)
        {
            isAvaliableLineUIActive = false;
            activeRoute = null;

            availableLineCanvasGroup.DOFade(0f,.3f).From(1f);
        }   
    }
}
