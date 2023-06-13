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
    public List<TileObject> listTiles = new List<TileObject>();

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
            gridSize = new Vector2Int(gridSize.x,gridSize.y);
        }

        transform.position = new Vector3(-((gridSize.x - 1) / 2f),0f, -((gridSize.y - 1) / 2f));

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
                Vector3 newPos = new Vector3(newCell.pos.x, newCell.pos.y,0f);
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
                
                if(diagonal)
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
    
    public Dictionary<string, int> ReadSaveInfo(string info)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        string[] gridState = info.Split(':');

        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                result.Add(x +","+y, int.Parse(gridState[x * gridSize.y + y]));
            }
        }
        return result;
    }
    
    public CellController GetCell(Vector2Int pos, bool autoClamp = true)
    {
        if(autoClamp)
        {
            pos = ClampPosToGrid(pos);
        }
        
        if(CheckIfPosInGrid(pos))
        {
            return cells[pos.x + "," + pos.y];
        }
        
        return null;
    }
    
    public CellController GetCell(int x, int y)
    {
        return cells[x + "," + y];
    }
    
    public CellController GetBelowCell(Vector2Int pos)
    {
        if (pos.y >= 1)
        {
            pos = ClampPosToGrid(pos + Vector2Int.down, true);
            return cells[pos.x + "," + pos.y];
        }
        
        return null;
    }

    public bool AreCellNeighbors(CellController first, CellController second, bool diagonal = false)
    {
        if (first.pos.x == second.pos.x)
        {
            if (first.pos.y + 1 == second.pos.y || first.pos.y - 1 == second.pos.y)
            {
                return true;
            }
        }

        if (first.pos.y == second.pos.y)
        {
            if (first.pos.x + 1 == second.pos.x || first.pos.x - 1 == second.pos.x)
            {
                return true;
            }
        }

        if (diagonal)
        {
            if (first.diagonalConnected.Contains(second))
            {
                return true;
            }
        }
        return false;
    }

    public TileController AddTileToCell(CellController cell)
    {
        Vector3 newPos = new Vector3(cell.pos.x, cell.pos.y, 0f);
        TileController newTile = Instantiate(tilePrefab,transform);
        newTile.transform.localPosition = newPos;
        listTiles.Add(newTile);

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

    public Vector2Int WordlToGridPos(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
    }

    public Vector2Int ClampPosToGrid(Vector2Int pos, bool loop = false)
    {
        int x = pos.x;
        int y = pos.y;

        if(x >= gridSize.x)
        {
            if(loop)
            {
                x = x - (gridSize.x);
            }
            else
            {
                x = gridSize.x - 1;
            }
        }
        if (x < 0)
        {
            if (loop)
            {
                x = gridSize.x - Mathf.Abs(x);
            }
            else
            {
                x = 0;
            }
        }

        if (y >= gridSize.y)
        {
            if (loop)
            {
                y = y - (gridSize.y - 1);
            }
            else
            {
                y = gridSize.y - 1;
            }
        }
        if (y < 0)
        {
            if (loop)
            {
                y = Mathf.Abs(y);
            }
            else
            {
                y = 0;
            }
        }

        return new Vector2Int(x,y);
    }

    public bool CheckIfPosInGrid(Vector2Int pos)
    {
        if (pos.y >= 0 && pos.x >= 0)
        {
            if (pos.y < gridSize.y && pos.x < gridSize.y)
            {
                return true;
            }
        }

        return false;
    }

    public void MoveTileTo(CellController from,CellController To)
    {
        from.linkedTile.cellParent = To;
        if(To.linkedTile != null)
        {

        }
        To.linkedTile = from.linkedTile;
        Vector3 newPos = new Vector3(To.pos.x, To.pos.y, 0f);
        To.linkedTile.transform.DOLocalMove(newPos,0.15f).SetEase(Ease.Linear)
        .OnComplete(() =>
        {

        });
        To.occupied = true;
        from.occupied = false;
        from.linkedTile = null;
    }

    public void UpdateCollum(List<int> collums)
    {
        for (int c = 0; c < collums.Count; c++)
        {
            CellController previousCell = null;
            for (int y = 0; y < gridSize.y; y++)
            {
                CellController currentCell = GetCell(collums[c], y);

                if (previousCell != null && (previousCell.occupied == false)
                    && currentCell.occupied )
                {
                    bool occupiedFound = false;
                    CellController check = previousCell;
                    while (occupiedFound == false)
                    {
                        CellController checkBellow = GetBelowCell(check.pos);
                        if (checkBellow == null)
                        {
                            break;
                        }

                        if (checkBellow.occupied == false)
                        {
                            check = checkBellow;
                        }
                        else
                        {
                            occupiedFound = true;
                        }


                    }
                    MoveTileTo(currentCell, check);
                }

                previousCell = currentCell;


            }
        }

    }

    public void UpdateCollum()
    {
        List<int> collums = new List<int>();
        for (int i = 0; i < gridSize.x; i++)
        {
            collums.Add(i);
        }

        UpdateCollum(collums);
    }

    public void ShuffleGrid()
    {
        List<CellController> listCellPos = new List<CellController>(mainSceneManager._GridController.cells.Values);
        List<TileObject> tiles = new List<TileObject>(mainSceneManager._GridController.listTiles);

        //Deleting from the list cells without tile
        List<CellController> cellsToRemove = new List<CellController>();
        for (int i = 0; i < listCellPos.Count; i++)
        {
            if (listCellPos[i].linkedTile == null)
            {
                cellsToRemove.Add(listCellPos[i]);
            }
        }
        for (int i = 0; i < cellsToRemove.Count; i++)
        {
            listCellPos.Remove(cellsToRemove[i]);
        }
        cellsToRemove.Clear();

        /*for (int i = 0; i < listCellPos.Count; i++)
        {
            print(listCellPos[i].pos);
        }*/

        while (listCellPos.Count > 0)
        {
            int randomNum = Random.Range(0, listCellPos.Count);
            int randomTileDest = Random.Range(0, listCellPos.Count);

            MoveTileToShuffleGrid(tiles[randomNum], listCellPos[randomTileDest]);
            tiles.RemoveAt(randomNum);
            listCellPos.RemoveAt(randomTileDest);
        }
    }

    public void MoveTileToShuffleGrid(TileObject from, CellController To)
    {
        Vector3 newPos = new Vector3(To.linkedTile.transform.localPosition.x, To.linkedTile.transform.localPosition.y, 0);
        from.transform.DOLocalMove(newPos, .5f).SetEase(Ease.OutBounce);
        from.cellParent = To;
        To.linkedTile = from;
    }

    
}
