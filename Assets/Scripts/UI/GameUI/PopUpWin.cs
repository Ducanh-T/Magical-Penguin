using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWin : MonoBehaviour
{
    public static PopUpWin instance;
    public CanvasGroup _canvasGroup;
    public Button btnHome, btnReplay, btnNextLevel;
    [SerializeField] ParticleSystem fireworkEffect;
    AudioSource audioSource;
    [SerializeField] AudioSource homeSound;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        btnHome.onClick.AddListener(GameManager.instance.Home);
        btnReplay.onClick.AddListener(GameManager.instance.Replay);
        btnNextLevel.onClick.AddListener(NextLevel);
    }
    public void NextLevel()
    {
        LevelManager.instance.NextLevel();
        Close();
    }
    public void Enable()
    {
        homeSound.Stop();
        audioSource.Play();
        _canvasGroup.blocksRaycasts = true;
        fireworkEffect.Play();
        UIManager.instance.OnWinterBG();
        _canvasGroup.DOFade(1, 0.3f).SetDelay(2);
    }

    public void Close()
    {
        audioSource.Stop();
        homeSound.Play();
        _canvasGroup.DOFade(0, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            UIManager.instance.OffWinterBG();
            _canvasGroup.blocksRaycasts = false;
        });
    }
}
