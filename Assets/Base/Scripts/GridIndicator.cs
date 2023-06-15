using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIndicator : MonoBehaviour
{
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

    public List<Vector2> listPosFirstSquare = new List<Vector2>();
    public List<Vector2> listPosSecondSquare = new List<Vector2>();
    public List<Vector2> listPosThirdSquare = new List<Vector2>();
    public List<Vector2> listPosFourthSquare = new List<Vector2>();
    public List<Vector2> listPosFifthSquare = new List<Vector2>();
    public List<Vector2> listPosSixthSquare = new List<Vector2>();
    public List<Vector2> listPosSeventhSquare = new List<Vector2>();
    public List<Vector2> listPosEighthSquare = new List<Vector2>();
    public List<Vector2> listPosNinthSquare = new List<Vector2>();

    public void Init()
    {
        AddToList(listPosFirstSquare,new []{new Vector2(0,0),new Vector2(0,1),new Vector2(0,2),new Vector2(1,0),new Vector2(1,1),new Vector2(1,2),new Vector2(2,0),new Vector2(2,1),new Vector2(2,2)});
        AddToList(listPosSecondSquare,new []{new Vector2(3,0),new Vector2(3,1),new Vector2(3,2),new Vector2(4,0),new Vector2(4,1),new Vector2(4,2),new Vector2(5,0),new Vector2(5,1),new Vector2(5,2)});
        AddToList(listPosThirdSquare,new []{new Vector2(6,0),new Vector2(6,1),new Vector2(6,2),new Vector2(7,0),new Vector2(7,1),new Vector2(7,2),new Vector2(8,0),new Vector2(8,1),new Vector2(8,2)});
        AddToList(listPosFourthSquare,new []{new Vector2(0,3),new Vector2(0,4),new Vector2(0,5),new Vector2(1,3),new Vector2(1,4),new Vector2(1,5),new Vector2(2,3),new Vector2(2,4),new Vector2(2,5)});
        AddToList(listPosFifthSquare,new []{new Vector2(3,3),new Vector2(3,4),new Vector2(3,5),new Vector2(4,3),new Vector2(4,4),new Vector2(4,5),new Vector2(5,3),new Vector2(5,4),new Vector2(5,5)});
        AddToList(listPosSixthSquare,new []{new Vector2(6,3),new Vector2(6,4),new Vector2(6,5),new Vector2(7,3),new Vector2(7,4),new Vector2(7,5),new Vector2(8,3),new Vector2(8,4),new Vector2(8,5)});
        AddToList(listPosSeventhSquare,new []{new Vector2(0,6),new Vector2(0,7),new Vector2(0,8),new Vector2(1,6),new Vector2(1,7),new Vector2(1,8),new Vector2(2,6),new Vector2(2,7),new Vector2(2,8)});
        AddToList(listPosEighthSquare,new []{new Vector2(3,6),new Vector2(3,7),new Vector2(3,8),new Vector2(4,6),new Vector2(4,7),new Vector2(4,8),new Vector2(5,6),new Vector2(5,7),new Vector2(5,8)});
        AddToList(listPosNinthSquare,new []{new Vector2(6,6),new Vector2(6,7),new Vector2(6,8),new Vector2(7,6),new Vector2(7,7),new Vector2(7,8),new Vector2(8,6),new Vector2(8,7),new Vector2(8,8)});
    }

    public void SelectAllLineColumn(CellController touchedCell) //Color cells that are in the same line and column as the touched
    {
        List<CellController> listColoredCells = new List<CellController>();
        
        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                if (currentCell.pos.y == touchedCell.pos.y && currentCell.pos != touchedCell.pos)
                {
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.sameLineColumnTile;
                    listColoredCells.Add(currentCell);
                }
                else if (currentCell.pos == touchedCell.pos)
                {
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.selectedTile;
                }
                else
                {
                    currentCell.linkedTile.spriteTile.color = Color.white;
                }
            }
        }
        
        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                if (currentCell.pos.x == touchedCell.pos.x && currentCell.pos != touchedCell.pos)
                {
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.sameLineColumnTile;
                }
                else if (currentCell.pos == touchedCell.pos)
                {
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.selectedTile;
                }
                else if(!listColoredCells.Contains(currentCell))
                {
                    currentCell.linkedTile.spriteTile.color = Color.white;
                }
            }
        }
    }

    public void SelectAllSquare(CellController touchedCell)
    {
        if (listPosFirstSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosFirstSquare,touchedCell);
        }
        else if(listPosSecondSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosSecondSquare,touchedCell);
        }
        else if(listPosThirdSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosThirdSquare,touchedCell);
        }
        else if(listPosFourthSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosFourthSquare,touchedCell);
        }
        else if(listPosFifthSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosFifthSquare,touchedCell);
        }
        else if(listPosSixthSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosSixthSquare,touchedCell);
        }
        else if(listPosSeventhSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosSeventhSquare,touchedCell);
        }
        else if(listPosEighthSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosEighthSquare,touchedCell);
        }
        else if(listPosNinthSquare.Contains(touchedCell.pos))
        {
            CheckIfCellInSquare(listPosNinthSquare,touchedCell);
        }
    } //Color cells that are in the square as the touched
    
    private void CheckIfCellInSquare(List<Vector2> listPos, CellController touchedCell) //Check for a given list if position of touched cell is in it
    {
        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                
                if (listPos.Contains(currentCell.pos) && currentCell.pos != touchedCell.pos)
                {
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.sameLineColumnTile;
                }
            }
        }
    }
    
    void AddToList(List<Vector2> listPos, Vector2[] arrayPos) 
    {
        for (int i = 0 ; i < arrayPos.Length ; i++ ) 
        {
            listPos.Add(arrayPos[i]);
        }
    }

    public void HighlightSameNumberOnGrid(TileController touchedTile) //Look for same number as clicked and set different color
    {
        for (int i = 0; i < mainSceneManager._GridController.listTiles.Count; i++)
        {

            if (mainSceneManager._GridController.listTiles[i].numberTile.text == touchedTile.numberTile.text && touchedTile.numberTile.text != " " && mainSceneManager._GridController.listTiles[i] != touchedTile)
            {
                mainSceneManager._GridController.listTiles[i].spriteTile.color = mainSceneManager._GridController.listTiles[i].sameNumber;
            }
        }
    }
}
