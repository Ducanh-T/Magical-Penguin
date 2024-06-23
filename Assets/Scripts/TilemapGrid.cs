using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGrid : MonoBehaviour
{
	public static TilemapGrid Instance;

	public Vector2 CellSize;


	private Grid grid;
	public Tilemap floor;
	private Tilemap wall;

	[SerializeField] private Tile colorTile, wallHot2, wallCold1, wallCold2, grassTile;

	private void Awake()
	{
		Instance = this;

		grid = GetComponent<Grid>();

		floor = transform.Find("Floor").GetComponent<Tilemap>();
		wall = transform.Find("Wall").GetComponent<Tilemap>();

		CellSize = grid.cellSize;

		InitGrid();
	}

	public void InitGrid()
	{
		floor.CompressBounds();
	}

	public Vector3Int WorldToCell(Vector2 worldPosition)
	{
		return grid.WorldToCell(worldPosition);
	}

	public Vector2 GetCellCenterWorld(Vector3Int cellPos)
	{
		return grid.GetCellCenterWorld(cellPos);
	}

	public Vector2 AlignToCell(Vector2 worldPosition)
	{
		Vector3Int cellPos = WorldToCell(worldPosition);
		return GetCellCenterWorld(cellPos);
	}

	public bool HasWall(Vector3Int cell)
	{
		return wall.HasTile(cell);
	}

	public void ChangeColorTile(Vector3Int cell)
	{
		if (floor.GetTile(cell) != colorTile)
		{
			floor.SetTile(cell, colorTile);
		}
	}

	public bool isWin()
	{
		foreach (var tile in floor.GetTilesBlock(floor.cellBounds))
		{
			if (tile != null && tile != colorTile) return false;
		}
		ChangeWall();
		return true;
	}

	public void ChangeWall()
	{
		for (int x = wall.cellBounds.xMin; x < wall.cellBounds.xMax; x++)
		{
			for (int y = wall.cellBounds.yMin; y < wall.cellBounds.yMax; y++)
			{
				Vector3Int cellPos = new Vector3Int(x, y, 0);
				Vector3 worldPos = GetCellCenterWorld(cellPos);

				if (wall.GetTile(cellPos) == null) continue;
				else if (wall.GetTile(cellPos) == wallHot2)
				{
					wall.SetTile(cellPos, wallCold2);
				}
				else wall.SetTile(cellPos, wallCold1);
			}
		}
	}
}