using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Game UI Vars")]
    public TextMeshProUGUI difficultyGameUIText;
    public TextMeshProUGUI timeGameUIText;
    public TextMeshProUGUI scoreGameUIText;

    [Header("Pause UI Vars")]
    public GameObject PauseUI;
    public TextMeshProUGUI timePauseUIText;

    [Header("Success Screen Vars")]
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

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

    public void OnExitButtonClicked()
    {
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
}
