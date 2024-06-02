using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private float displayTime = 1;
    private Transform board;
    private CanvasGroup canvasGroup;
    private float endAlpha;
    private Image image;
    private Animator boardAnimator;
    private readonly int tutorialState = Animator.StringToHash("Tutorial");
    private readonly int stopState = Animator.StringToHash("Stop");

    private void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        board = transform.Find("Board");
        endAlpha = image.color.a;
        boardAnimator = transform.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        board.localScale = new Vector3(0, 0, 1);
        canvasGroup.alpha = 0;
        boardAnimator.CrossFade(stopState, 0);
    }


    public void Hide()
    {
        image.DOFade(0, displayTime / 2f);
        board.DOScale(Vector3.zero, displayTime / 2f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1;
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                boardAnimator.CrossFade(stopState, 0);
            });
    }



    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(Display(() =>
        {
            boardAnimator.CrossFade(tutorialState, 0);
        }));
    }

    private IEnumerator Display(Action onComplete = null)
    {
        yield return image.DOFade(endAlpha, displayTime / 3).SetUpdate(true).WaitForCompletion();

        float elapsedTime = 0;
        while (elapsedTime < displayTime / 2)
        {
            float scale = Mathf.Lerp(0, 1, elapsedTime / (displayTime / 2));
            board.localScale = new Vector3(scale, scale, 1);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }
}
