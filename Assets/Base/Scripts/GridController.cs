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
        if(GameManager.Instance.savedGame == 0)
        {
            SetGridNumber(GameManager.Instance.gameModeString);
        }        
        else //Adding saved numbers, errors, timer, difficulty level
        {
            RestoreAllSavedVars();
        }
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
        
        this.Invoke(mainSceneManager.OnSuccess,0.5f);
    }
    

    //Save functions-------------------------------------

    private void RestoreAllSavedVars()
    {
        AddSavedNumbersToGrid();

        mainSceneManager.uiManager.difficultyGameUIText.text = PlayerPrefs.GetString("gameDifficulty");
        mainSceneManager._Timer.timeValue += PlayerPrefs.GetFloat("timer");
            
        //Hints
        mainSceneManager.uiManager.counterHint = PlayerPrefs.GetInt("numberHint");
        mainSceneManager.uiManager.counterText.text = mainSceneManager.uiManager.counterHint.ToString();
        if (mainSceneManager.uiManager.counterHint == 0)
            mainSceneManager.uiManager.hintButton.interactable = false;
            
        //Errors count
        if (mainSceneManager._solutionController.counterErrors != PlayerPrefs.GetInt("numberErrors"))
        {
            mainSceneManager._solutionController.counterErrors = 0;
            mainSceneManager._solutionController.counterErrors = PlayerPrefs.GetInt("numberErrors");
            for (int i = 0; i < mainSceneManager._solutionController.counterErrors; i++)
            {
                mainSceneManager._solutionController.imageCrosses[i].color = mainSceneManager._solutionController.redCross;
            }
        }
        
        //Numbers UI
        for (int i = 0; i < mainSceneManager.uiManager.listNumbersGameObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("numberUIActive_" + i) == 0)
            {
                mainSceneManager.uiManager.listNumbersGameObjects[i].SetActive(false);
            }
        }
    }
    
    public void SaveGame()
    {
        //Saving the difficulty of the level
        switch (GameManager.Instance._GameMode)
        {
            case GAME_MODE.EASY:
                PlayerPrefs.SetString("gameDifficulty","Easy");
                break;
            case GAME_MODE.MEDIUM:
                PlayerPrefs.SetString("gameDifficulty","Medium");
                break;
            case GAME_MODE.HARD:
                PlayerPrefs.SetString("gameDifficulty","Hard");
                break;
        }
        
        //Saving which numbers are active
        for (int i = 0; i < mainSceneManager.uiManager.listNumbersGameObjects.Count; i++)
        {
            if(mainSceneManager.uiManager.listNumbersGameObjects[i].activeSelf)
                PlayerPrefs.SetInt("numberUIActive_" + i,1);
            else
                PlayerPrefs.SetInt("numberUIActive_" + i,0);
        }
        
        for (int i = 0; i < listTiles.Count; i++)
        {
            //Saving to the list numbers in order in the grid
            if(listTiles[i].numberTile.text != " ")
                PlayerPrefs.SetInt("savedIntList_" + i,int.Parse(listTiles[i].numberTile.text));
            else
                PlayerPrefs.SetInt("savedIntList_" + i,0);

            //Saving to the list correct numbers in order in the grid
            PlayerPrefs.SetInt("savedCorrectNumberList_" + i,listTiles[i].correctNumber);
            
            //Saving to the list if they are default or not
            if(listTiles[i].defaultValue)
                PlayerPrefs.SetInt("listDefaultValue_" + i,1);
            else
                PlayerPrefs.SetInt("listDefaultValue_" + i,0);
            
            //Saving number hints left
            PlayerPrefs.SetInt("numberHint",mainSceneManager.uiManager.counterHint);
            
            //Saving num errors
            PlayerPrefs.SetInt("numberErrors",mainSceneManager._solutionController.counterErrors);
            
            //Saving the timer
            PlayerPrefs.SetFloat("timer",mainSceneManager._Timer.timeValue);
        }

        mainSceneManager._touchController.touchedTile = null;
        PlayerPrefs.SetInt("savedGame",1);
    }

    private void AddSavedNumbersToGrid()
    {
        for (int i = 0; i < mainSceneManager._GridController.listTiles.Count; i++)
        {
            int intToAdd = PlayerPrefs.GetInt("savedIntList_" + i);
            int correctNumber = PlayerPrefs.GetInt("savedCorrectNumberList_" + i);
            int isDefaultValue = PlayerPrefs.GetInt("listDefaultValue_" + i);
            
            if(isDefaultValue == 1)
                mainSceneManager._GridController.listTiles[i].defaultValue = true;

            mainSceneManager._GridController.listTiles[i].SetCorrectNumber(correctNumber);
            mainSceneManager._GridController.listTiles[i].SetNumber(intToAdd);
            mainSceneManager._GridController.listTiles[i].spriteTile.color = Color.white;
        }
    }
    
    private void OnApplicationQuit()
    {
        SaveGame();    
    }
}