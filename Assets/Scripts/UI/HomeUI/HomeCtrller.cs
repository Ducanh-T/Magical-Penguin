using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeCtrller : MonoBehaviour
{
    public static HomeCtrller instance;
    private Button playButton;
    private Transform playBtn;
    private Transform guideBtn;

    private void Awake()
    {
        instance = this;
        playButton = transform.Find("PlayBtn").GetComponent<Button>();
        playBtn = transform.Find("PlayBtn");
        guideBtn = transform.Find("GuideBtn");
    }

    private void Start()
    {
        playButton.onClick.AddListener(LoadGameScene);
        StartCoroutine(PunchPosPlayBtn());
    }

    public void LoadGameScene()
    {
        BlackScreen.Instance.In(() => SceneManager.LoadScene("Game"));
    }

    private IEnumerator PunchPosPlayBtn()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            Vector2 startPos1 = playBtn.position;
            playBtn.DOPunchPosition(new Vector2(playBtn.position.x - 10, startPos1.y), 0.5f);

            yield return new WaitForSeconds(1f);
            Vector2 startPos2 = guideBtn.position;
            guideBtn.DOPunchPosition(new Vector2(guideBtn.position.x - 10, startPos2.y), 0.5f);
        }
    }
}
