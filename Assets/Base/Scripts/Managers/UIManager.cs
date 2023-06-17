using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //UI
    public GameObject GameUI;
    public GameObject SuccesUI;
    public GameObject GameOverUI;

    private GameManager _gameManager;
    private GameManager gameManager
    {
        get
        {
            if (_gameManager != null)
            {
                return _gameManager;
            }
            else
            {
                _gameManager = GameManager.Instance;
            }
            return _gameManager;
        }

        set
        {
            _gameManager = value;
        }
    }
    
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

    [Header("Numbers UI")] 
    public List<GameObject> listNumbersGameObjects = new List<GameObject>();

    [Header("Game UI Vars")]
    public TextMeshProUGUI difficultyGameUIText;
    public TextMeshProUGUI timeGameUIText;

    [Header("Pause UI Vars")]
    public GameObject PauseUI;
    public TextMeshProUGUI timePauseUIText;

    [Header("Success Screen Vars")]
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI timeText;
    
    [Header("Notes Vars")]
    public GameObject onImage;
    public GameObject offImage;

    [Header("Hint Vars")] 
    public Button hintButton;
    public TextMeshProUGUI counterText;
    public int counterHint = 3;

    public void OnStartButtonClicked()
    {
        GameUI.SetActive(true);
    }

    public void OnNextLevelButtonClicked()
    {
        gameManager._currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", gameManager._currentLevel);

        gameManager.levelToPlay++;
        PlayerPrefs.SetInt("LevelToPlay", gameManager.levelToPlay);

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    
    public void OnRetryButtonClicked()
    {
        PlayerPrefs.SetInt("savedGame",0);
        GameManager.Instance.savedGame = PlayerPrefs.GetInt("savedGame");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnExitButtonClicked()
    {
        mainSceneManager._GridController.SaveGame();
        PlayerPrefs.SetInt("savedGame",1);
        GameManager.Instance.savedGame = PlayerPrefs.GetInt("savedGame");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnPauseButtonClicked()
    {
        mainSceneManager.pausedGame = true;
        mainSceneManager._Timer.DisplayTime(mainSceneManager._Timer.timeValue, timePauseUIText);
        PauseUI.SetActive(true);
    }

    public void OnResumeButtonClicked()
    {
        mainSceneManager.pausedGame = false;
        PauseUI.SetActive(false);
    }

    public void OnNotesButtonClicked()
    {
        if (mainSceneManager.notesModeActive)
        {
            offImage.SetActive(true);
            onImage.SetActive(false);
            mainSceneManager.notesModeActive = false;
        }
        else
        {
            onImage.SetActive(true);
            offImage.SetActive(false);
            mainSceneManager.notesModeActive = true;
        }
    }

    public void OnEraseButtonClicked()
    {
        if(mainSceneManager._touchController.touchedTile == null || mainSceneManager._touchController.touchedTile.defaultValue) return;

        mainSceneManager._touchController.touchedTile.SetNumber(0);
        for (int i = 0; i < mainSceneManager._touchController.touchedTile.numbersNotes.Count; i++) //Hiding all the notes of the tile
        {
            if(mainSceneManager._touchController.touchedTile.numbersNotes[i].activeSelf)
                mainSceneManager._touchController.touchedTile.numbersNotes[i].SetActive(false);
        }
    }

    public void OnHintButtonClicked()
    {
        counterHint--;
        if(counterHint == 0)
        {
            hintButton.interactable = false;
        }
        counterText.text = counterHint.ToString();

        int randomInt;
        
        randomInt = Random.Range(0, mainSceneManager._GridController.listTiles.Count);

        while (mainSceneManager._GridController.listTiles[randomInt].numberTile.text != " ")
        {
            randomInt = Random.Range(0, mainSceneManager._GridController.listTiles.Count);
        }
        
        mainSceneManager._GridController.listTiles[randomInt].SetNumber(mainSceneManager._GridController.listTiles[randomInt].correctNumber);
        mainSceneManager._touchController.touchedTile = mainSceneManager._GridController.listTiles[randomInt];
        
        mainSceneManager._gridIndicator.SelectAllLineColumn(mainSceneManager._touchController.touchedTile.cellParent);
        mainSceneManager._gridIndicator.SelectAllSquare(mainSceneManager._touchController.touchedTile.cellParent);
        mainSceneManager._gridIndicator.HighlightSameNumberOnGrid(mainSceneManager._touchController.touchedTile);
    }
}
