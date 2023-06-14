using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainSceneManager : SceneContextManager
{
    public CameraController _cameraController;
    public GridController _GridController;
    public UIManager uiManager;
    public TouchController _touchController;
    public SolutionController _solutionController;
    public Timer _Timer;

    [HideInInspector] public bool pausedGame = false;

    void Start()
    {
        base.Start();

        if (gameManager._LevelManager != null)
        {
            long levelCount = gameManager._LevelManager.listLevels.Count;
            if (gameManager.levelToPlay > levelCount - 1)
            {
                gameManager.levelToPlay = 0;
            }
        }

        Init();
    }

    private void Init()
    {
        if (gameManager._currentLevel == 0)
        {
           
        }
        else
        { 
            uiManager.OnStartButtonClicked();
        }
        
        gameHasStarted = true;
        _GridController.Init();
        _cameraController.Init();

        uiManager.difficultyGameUIText.text = gameManager.gameModeString.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            OnSuccess();
        }
    }

    public void OnSuccess()
    {
        if (uiManager.GameUI != null)
            uiManager.GameUI.SetActive(false);
        if (uiManager.SuccesUI != null)
            uiManager.SuccesUI.SetActive(true);

        gameHasStarted = false;
        isVictory = true;

        uiManager.difficultyText.text = gameManager.gameModeString.ToString();
        //uiManager.timeText.text = 
        //uiManager.scoreText.text = 
    }

    public void OnGameOver()
    {
        if (uiManager.GameUI != null)
            uiManager.GameUI.SetActive(false);
        if (uiManager.GameOverUI != null)
            uiManager.GameOverUI.SetActive(true);

        gameHasStarted = false;
        isVictory = false;
    }
}
