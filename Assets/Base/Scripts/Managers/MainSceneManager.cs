using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainSceneManager : SceneContextManager
{
    public CameraController _cameraController;
    public GridController _GridController;
    public UIManager uiManager;

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
    }

    public void OnSuccess()
    {
        if (uiManager.GameUI != null)
            uiManager.GameUI.SetActive(false);
        if (uiManager.SuccesUI != null)
            uiManager.SuccesUI.SetActive(true);

        gameHasStarted = false;
        isVictory = true;
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