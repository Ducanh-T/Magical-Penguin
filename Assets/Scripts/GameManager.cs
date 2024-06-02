using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        instance = this;
    }

    public void Replay()
    {
        BlackScreen.Instance.In( () => SceneManager.LoadScene("Game"));
    }

    public void Home()
    {
        BlackScreen.Instance.In( () => SceneManager.LoadScene("Home"));
    }

    public void CheckLose()
    {
        if (LevelManager.instance.currentLevelMove <= 0)
        {
            Lose();
        }
        else return;
    }

    public void Win()
    {
        PlayerCtrller.instance.canMove = false;
        PopUpWin.instance.Enable();
    }

    public void Lose()
    {
        PlayerCtrller.instance.canMove = false;
        PopUpLose.instance.Enable();
    }
}
