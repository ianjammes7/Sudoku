using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using Lofelt.NiceVibrations;
using UnityEditor;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public CellController cellPrefab;
    public TileController tilePrefab;

    public Dictionary<string, CellController> cells = new Dictionary<string, CellController>();
    public List<TileController> listTiles = new List<TileController>();

    private MainSceneManager _mainSceneManager;
    protected MainSceneManager mainSceneManager
    {
        get
        {
            if (_mainSceneManager != null)
            {
                return _mainSceneManager;
            }
            else
            {
                _mainSceneManager = GameManager.Instance.currentSceneManager as MainSceneManager;
            }
            return _mainSceneManager;
        }

        set
        {
            _mainSceneManager = value;
        }
    }

    public void Init()
    {
        //Defining size for levels
        if (GameManager.Instance._currentLevel >= 0)
        {
            gridSize = new Vector2Int(gridSize.x, gridSize.y);
        }

        transform.position = new Vector3(-((gridSize.x - 1) / 2f), 0f, -((gridSize.y - 1) / 2f));

        CreateGrid();
    }

    public void CreateGrid(String gridLayout = "none")
    {

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                CellController newCell = Instantiate(cellPrefab, this.transform);
                newCell.pos = new Vector2Int(x, y);
                Vector3 newPos = new Vector3(newCell.pos.x, newCell.pos.y, 0f);
                newCell.transform.localPosition = newPos;
                cells.Add((x + "," + y), newCell);

                AddTileToCell(newCell);
            }
        }

        UpdateNeigbours();
    }
    public void UpdateNeigbours(bool diagonal = false)
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                CellController currentCell = cells[(x + "," + y)];

                if (x > 0)
                    currentCell.connectedCells.Add(cells[(x - 1) + "," + y]);
                if (x < gridSize.x - 1)
                    currentCell.connectedCells.Add(cells[(x + 1) + "," + y]);
                if (y > 0)
                    currentCell.connectedCells.Add(cells[x + "," + (y - 1)]);
                if (y < gridSize.y - 1)
                    currentCell.connectedCells.Add(cells[x + "," + (y + 1)]);

                if (diagonal)
                {
                    if (x > 0 && y > 0)
                        currentCell.diagonalConnected.Add(cells[(x - 1) + "," + (y - 1)]);
                    if (x < gridSize.x - 1 && y < gridSize.y - 1)
                        currentCell.diagonalConnected.Add(cells[(x + 1) + "," + (y + 1)]);
                    if (x > 0 && y < gridSize.y - 1)
                        currentCell.diagonalConnected.Add(cells[(x - 1) + "," + (y + 1)]);
                    if (x < gridSize.x - 1 && y > 0)
                        currentCell.diagonalConnected.Add(cells[(x + 1) + "," + (y - 1)]);
                }
            }
        }
    }

    public TileController AddTileToCell(CellController cell)
    {
        Vector3 newPos = new Vector3(cell.pos.x, cell.pos.y, 0f);
        TileController newTile = Instantiate(tilePrefab, transform);
        newTile.transform.localPosition = newPos;
        listTiles.Add(newTile);
        newTile.SetNumber();

        cell.linkedTile = newTile;
        cell.occupied = true;
        newTile.cellParent = cell;

        return newTile;
    }

    public void DestroyTileAt(CellController cell)
    {
        if (cell.linkedTile != null)
        {
            listTiles.Remove(cell.linkedTile);
            Destroy(cell.linkedTile.gameObject);
            cell.linkedTile = null;
            cell.occupied = false;
        }
    }
}