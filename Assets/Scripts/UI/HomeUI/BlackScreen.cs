using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public static BlackScreen Instance;

    [SerializeField] private float fadeTime = 0.5f;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 1;
        Out();
    }

    public void In(Action onComplete = null)
    {
        canvasGroup.DOFade(1, fadeTime).SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }

    public void Out(Action onComplete = null)
    {
        canvasGroup.DOFade(0, fadeTime).SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
    }
}
