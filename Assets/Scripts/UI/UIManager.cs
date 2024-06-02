using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Text txtLevel, txtMaxMove;
    [SerializeField] private CanvasGroup winterBG;

    private void Awake() {
        instance = this;
    }

    public void UpdateTextLevel(int level)
    {
        txtLevel.text = "Level " + level;
    }

    public void UpdateTxtMaxMove(int currentMove)
    {
        txtMaxMove.text = "Max of Move: " + currentMove;
    }

    public void OnWinterBG()
    {
        winterBG.DOFade(1, 1.5f);
    }

    public void OffWinterBG()
    {
        winterBG.alpha = 0;
    }
}
