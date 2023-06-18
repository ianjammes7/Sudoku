using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lofelt.NiceVibrations;
using TMPro;
using UnityEngine;

public class TileController : MonoBehaviour
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

    [HideInInspector] public CellController cellParent;
     public int correctNumber;
     public bool defaultValue;

    public TextMeshPro numberTile;

    public SpriteRenderer spriteTile;

    public List<GameObject> numbersNotes = new List<GameObject>();

    [Header("Colors")]
    public Color selectedTile;
    public Color sameLineColumnTile;
    public Color sameNumber;
    public Color correctTextColor;
    public Color wrongTextColor;

    public void SetNumber(int num)
    {
        for (int i = 0; i < numbersNotes.Count; i++) //Hiding all the notes of the tile
        {
            if(numbersNotes[i].activeSelf)
                numbersNotes[i].SetActive(false);
        }
        
        numberTile.text = num.ToString();
        if(num == 0) //Empty tile if it is a 0
        {
            numberTile.text = " ";
        }

        if (defaultValue)
            return;

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        
        if (num != correctNumber && num != 0)
        {
            numberTile.color = wrongTextColor;
            mainSceneManager._solutionController.WrongNumber();
        }
        else if(num != 0)
        {
            numberTile.color = correctTextColor;
            mainSceneManager._gridIndicator.HighlightSameNumberOnGrid(this);
            if(mainSceneManager._touchController.touchedTile != null)
            {
                StartCoroutine(CheckIfLineCompleted(mainSceneManager._touchController.touchedTile.cellParent));
                StartCoroutine(CheckIfColumnCompleted(mainSceneManager._touchController.touchedTile.cellParent));
                CheckIfSquareCompleted(mainSceneManager._touchController.touchedTile.cellParent);
                CheckIfNumberCompleted(num);
            }            
            defaultValue = true;
        }

        if(mainSceneManager._touchController.touchedTile != null)
            mainSceneManager._touchController.touchedTile.spriteTile.color = selectedTile;

        mainSceneManager._GridController.CheckForCompleteSudoku();
    }
    
    public void SetNoteNumber(int num) //Add a number as a note to the tile
    {
        if(numbersNotes[num-1].activeSelf)
            numbersNotes[num-1].SetActive(false);
        else
            numbersNotes[num-1].SetActive(true);
    }

    public void SetCorrectNumber(int number) //Set which is the correct number for this tile
    {
        correctNumber = number;
    }
    
    private void CheckIfNumberCompleted(int number) //Check if all the same numbers as this one are completed in the grid and delete from input
    {
        int counter = 0;
        
        for (int i = 0; i < mainSceneManager._GridController.listTiles.Count; i++)
        {
            if (mainSceneManager._GridController.listTiles[i].numberTile.text == number.ToString())
            {
                counter++;
            }
        }

        if (counter == 9)
        {
            mainSceneManager.uiManager.listNumbersGameObjects[number-1].SetActive(false);
        }
    }

    IEnumerator CheckIfLineCompleted (CellController touchedCell)
    {
        List<CellController> listCellsSameLine = new List<CellController>();
        
        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                if (currentCell.pos.y == touchedCell.pos.y)
                {
                    if (currentCell.linkedTile.numberTile.text == currentCell.linkedTile.correctNumber.ToString())
                    {
                        listCellsSameLine.Add(currentCell);   
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        for (int i = 0; i < listCellsSameLine.Count; i++)
        {
            listCellsSameLine[i].linkedTile.spriteTile.DOColor(selectedTile,0.1f).SetLoops(2,LoopType.Yoyo);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    IEnumerator CheckIfColumnCompleted (CellController touchedCell)
    {
        List<CellController> listCellsSameColumn = new List<CellController>();
        
        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                if (currentCell.pos.x == touchedCell.pos.x)
                {
                    if (currentCell.linkedTile.numberTile.text == currentCell.linkedTile.correctNumber.ToString())
                    {
                        listCellsSameColumn.Add(currentCell);   
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }

        for (int i = 0; i < listCellsSameColumn.Count; i++)
        {
            listCellsSameColumn[i].linkedTile.spriteTile.DOColor(selectedTile,0.1f).SetLoops(2,LoopType.Yoyo);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    //Functions to make anim if square is completed------------------------------
    private void CheckIfSquareCompleted(CellController touchedCell)
    {
        if (mainSceneManager._gridIndicator.listPosFirstSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosFirstSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosSecondSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosSecondSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosThirdSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosThirdSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosFourthSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosFourthSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosFifthSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosFifthSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosSixthSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosSixthSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosSeventhSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosSeventhSquare,touchedCell));
        }
        else if(mainSceneManager._gridIndicator.listPosEighthSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosEighthSquare,touchedCell));

        }
        else if(mainSceneManager._gridIndicator.listPosNinthSquare.Contains(touchedCell.pos))
        {
            StartCoroutine(CheckIfCellInSquareAndCompleted(mainSceneManager._gridIndicator.listPosNinthSquare,touchedCell));
        }
    } 
    
    IEnumerator CheckIfCellInSquareAndCompleted(List<Vector2> listPos, CellController touchedCell) //Check for a given list if position of touched cell is in it
    {
        List<CellController> listCellsSameSquare = new List<CellController>();

        for (int y = mainSceneManager._GridController.gridSize.y; y > 0; y--)
        {
            for (int x = 0; x < mainSceneManager._GridController.gridSize.x; x++)
            {
                CellController currentCell = mainSceneManager._GridController.cells[x + "," + y];
                
                if (listPos.Contains(currentCell.pos))
                {
                    if (currentCell.linkedTile.numberTile.text == currentCell.linkedTile.correctNumber.ToString())
                    {
                        listCellsSameSquare.Add(currentCell);   
                    }
                    else
                    {
                        yield break;
                    }
                    currentCell.linkedTile.spriteTile.color = currentCell.linkedTile.sameLineColumnTile;
                }
            }
        }
        
        for (int i = 0; i < listCellsSameSquare.Count; i++)
        {
            listCellsSameSquare[i].linkedTile.spriteTile.DOColor(selectedTile,0.1f).SetLoops(2,LoopType.Yoyo);
            yield return new WaitForSeconds(0.05f);
        }
    }
}