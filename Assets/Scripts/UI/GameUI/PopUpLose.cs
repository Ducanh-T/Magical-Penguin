using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopUpLose : MonoBehaviour
{
    public static PopUpLose instance;  
    public   CanvasGroup _canvasGroup;
    public Button btnHome, btnReplay;
    AudioSource audioSource;
    [SerializeField] AudioSource homeSound;
    private void Awake()
    {
        instance = this ; 
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        btnHome.onClick.AddListener(GameManager.instance.Home);
        btnReplay.onClick.AddListener(GameManager.instance.Replay);
       
    }
    public void Enable()
    {
        _canvasGroup.blocksRaycasts = true;
        homeSound.Stop();
        audioSource.Play();
        _canvasGroup.DOFade(1, 0.3f).SetDelay(2f);
    }
    public void Close()
    {
        audioSource.Stop();
        homeSound.Play();
        _canvasGroup.DOFade(0, 0.3f).OnComplete(() =>
        {
            _canvasGroup.blocksRaycasts = false;
        });
    }
}
