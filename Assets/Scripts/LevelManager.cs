using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private GameObject[] mapLevel;
    public int levelIndex;
    private int levelUI;
    private int maxLevel;
    public Transform currentLevel;
    public CanvasGroup blackScreen;

    [SerializeField] private GameObject player;

    // private int[] maxNumMove = { 9, 12, 13, 25, 25, 26, 20, 29, 12, 10, 23, 27, 27, 18, 23};
    private int[] maxNumMove = { 10, 9, 12, 12, 13, 20, 25, 18, 26, 27, 27, 23, 25, 23, 29};
    public int currentLevelMove;

    private void Awake()
    {
        instance = this;
        maxLevel = maxNumMove.Count() - 1;
    }

    private void Start()
    {
        levelIndex = GetSavedLevelIndex();
        levelUI = PlayerPrefs.GetInt("LevelUI", 1);
        UIManager.instance.UpdateTextLevel(levelUI);
        SpawnMap();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            NextLevel();
        }
    }

    private int GetSavedLevelIndex()
    {
        if (!PlayerPrefs.HasKey("level")) return 0;

        int savedLevel = PlayerPrefs.GetInt("level");
        return savedLevel > maxLevel ? 0 : savedLevel;
    }

    private void SpawnMap()
    {
        if (levelIndex > maxLevel) levelIndex = 0;
        SetMaxMove(levelIndex);
        currentLevel = Instantiate(mapLevel[levelIndex], transform).transform;
        player.transform.position = currentLevel.Find("StartPos").position;
        player.GetComponent<PlayerCtrller>().AlignPlayerToCell();
        player.GetComponent<PlayerCtrller>().canMove = true;
        // Debug.Log("Level " + (levelIndex + 1) + " - " + currentLevel.Find("Grid").GetComponent<TilemapGrid>().CountTile());
    }

    public void NextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(NextLevelRoutines());
    }

    private IEnumerator NextLevelRoutines()
    {
        yield return blackScreen.DOFade(1f, 0.5f).WaitForCompletion();
        Destroy(currentLevel.gameObject);
        SaveData();
        UIManager.instance.UpdateTextLevel(levelUI);
        SpawnMap();
        blackScreen.DOFade(0f, 0.5f);
    }


    private void SetMaxMove(int levelIndex)
    {
        // reset bien dem so lan di chuyen, set lai so lan di chuyen max
        currentLevelMove = maxNumMove[levelIndex];
        UIManager.instance.UpdateTxtMaxMove(currentLevelMove);
    }

    public void ReduceMoveCount()
    {
        currentLevelMove--;
        UIManager.instance.UpdateTxtMaxMove(currentLevelMove);
    }

    public void SaveData()
    {
        levelIndex++;
        levelUI++;
        PlayerPrefs.SetInt("level", levelIndex);
        PlayerPrefs.SetInt("LevelUI", levelUI);
    }
}
