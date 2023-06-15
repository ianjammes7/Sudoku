using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    private int selected_grid_data = -1;

    public void Init()
    {
        //Defining size for levels
        if (GameManager.Instance._currentLevel >= 0)
        {
            gridSize = new Vector2Int(gridSize.x, gridSize.y);
        }

        transform.position = new Vector3(-((gridSize.x - 1) / 2f), 0f, -((gridSize.y - 1) / 2f));

        CreateGrid();
        SetGridNumber(GameManager.Instance.gameModeString);
    }

    public float squareGap = 0.1f;

    public void CreateGrid(String gridLayout = "none")
    {
        Vector2 gap_number = new Vector2(0,1);
        bool rowMoved = false;
            
        for (int y = gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                CellController newCell = Instantiate(cellPrefab, this.transform);
                newCell.pos = new Vector2Int(x, y-1);
                Vector3 newPos = new Vector3(newCell.pos.x + (gap_number.x * squareGap), newCell.pos.y - (gap_number.y * squareGap), 0f);

                if(x > 0 && x%3 == 0)
                {
                    gap_number.x++;
                    newPos.x += squareGap;
                }

                if (y > 0 && y % 3 == 0 && rowMoved == false)
                {
                    rowMoved = true;
                    gap_number.y++;
                    newPos.y -= squareGap;
                }

                newCell.transform.localPosition = newPos;
                cells.Add((x + "," + y), newCell);

                AddTileToCell(newCell);
            }

            gap_number.x = 0;
            rowMoved = false;
        }
    }

    public TileController AddTileToCell(CellController cell) //Adding the tile to every cell of the grid
    {
        Vector3 newPos = new Vector3(cell.pos.x, cell.pos.y, 0f);
        TileController newTile = Instantiate(tilePrefab, transform);
        newTile.transform.localPosition = cell.transform.localPosition;
        listTiles.Add(newTile);

        cell.linkedTile = newTile;
        cell.occupied = true;
        newTile.cellParent = cell;

        return newTile;
    }

    public void SetGridNumber(string level) //Setting the initial numbers on the grid
    {
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][selected_grid_data];

        SetGridSquareData(data);
    }

    private void SetGridSquareData(SudokuData.SudokuBoardData data) //Going through thw whole grid and setting the initial number and the wanted number for each tile
    {
        for (int i = 0; i < mainSceneManager._GridController.listTiles.Count; i++)
        {
            if(data.unsolved_Data[i] != 0)
                mainSceneManager._GridController.listTiles[i].defaultValue = true;

            mainSceneManager._GridController.listTiles[i].SetNumber(data.unsolved_Data[i]);
            mainSceneManager._GridController.listTiles[i].SetCorrectNumber(data.solved_Data[i]);
        }
    }

    public void CheckForCompleteSudoku()
    {
        for (int i = 0; i < listTiles.Count; i++)
        {
            if (listTiles[i].numberTile.text == " ") //if there is at least an empty tile
            {
                return;
            }
        }
        
        Invoke(nameof(mainSceneManager.OnSuccess),0.5f);
    }

}