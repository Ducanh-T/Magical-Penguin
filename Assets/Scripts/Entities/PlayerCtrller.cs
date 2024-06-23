using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCtrller : MonoBehaviour
{
    public static PlayerCtrller instance;

    private TilemapGrid tilemapGrid => TilemapGrid.Instance;
    Vector3 startMousePos;
    Vector3Int startCell;
    bool isDrag;
    private bool isMoving;
    [SerializeField] float dragThreshold;
    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    public bool canMove;
    AudioSource playerAudioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
        canMove = true;
    }

    private void Update()
    {
        if (canMove == true && !isMoving && Input.GetMouseButtonDown(0))
        {
            StartDrag();
            startCell = tilemapGrid.floor.WorldToCell(Position);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }

        if (isDrag)
            OnDraging();
    }

    private void StartDrag()
    {
        startMousePos = Input.mousePosition;
        isDrag = true;
    }

    private void OnDraging()
    {
        if (!isDrag) return;
        Vector3 currentMousePos = Input.mousePosition;
        if (Vector3.Distance(currentMousePos, startMousePos) < dragThreshold) return;

        Vector3 dragDirection = RoundToNearestDirection((currentMousePos - startMousePos).normalized);

        MoveByDirection(dragDirection);
    }

    private void MoveByDirection(Vector3 dragDirection)
    {
        isDrag = false;

        Vector3Int direction = Vector3Int.RoundToInt(dragDirection);
        Vector3Int currentCell = tilemapGrid.WorldToCell(Position);

        int count = 0;
        while (CanMoveToCell(currentCell + direction) && count < 100)
        {
            currentCell += direction;
            count++;
        }
        
        if (count > 90) Debug.LogError("Count: " + count);

        if(startCell == currentCell) return;
        
        LevelManager.instance.ReduceMoveCount();

        Vector2 moveDestination = tilemapGrid.GetCellCenterWorld(currentCell);

        float duration = 0.05f * count;

        isMoving = true;
        playerAudioSource.Play();
        transform.DOMove(moveDestination, duration)
            .OnComplete(() => {
                isMoving = false;
                if(tilemapGrid.isWin()) GameManager.instance.Win();
                else GameManager.instance.CheckLose();
            });
        StartCoroutine(CheckPos());
    }



    private bool CanMoveToCell(Vector3Int cell)
    {
        return !tilemapGrid.HasWall(cell);
    }

    public Vector2 RoundToNearestDirection(Vector2 inputVector)
    {
        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

        angle = Mathf.Round(angle / 90) * 90;

        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    private IEnumerator CheckPos()
    {
        while(isMoving)
        {
            Vector3Int cell = tilemapGrid.WorldToCell(Position);
            tilemapGrid.ChangeColorTile(cell);
            yield return null;
        }
    }

    public void AlignPlayerToCell()
    {
        Position = tilemapGrid.AlignToCell(Position);
    }
}
