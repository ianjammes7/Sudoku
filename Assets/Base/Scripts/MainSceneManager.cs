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
                SaveManager.SetLevelToPlay(gameManager.levelToPlay);
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

        TinySauce.OnGameStarted(levelNumber:gameManager._currentLevel.ToString());
    }
    override public void OnSuccess()
    {
        if (uiManager.GameUI != null)
            uiManager.GameUI.SetActive(false);
        if (uiManager.SuccesUI != null)
            uiManager.SuccesUI.SetActive(true);

        gameHasStarted = false;
        isVictory = true;

        base.OnSuccess();
        //TinySauce.OnGameFinished(isVictory,totalFruits,levelNumber:_currentLevel.ToString());
        //MMVibrationManager.Haptic(HapticTypes.Success);
    }

    override public void OnGameOver()
    {
        if (uiManager.GameUI != null)
            uiManager.GameUI.SetActive(false);
        if (uiManager.GameOverUI != null)
            uiManager.GameOverUI.SetActive(true);

        gameHasStarted = false;
        isVictory = false;

        base.OnGameOver();
        //MMVibrationManager.Haptic(HapticTypes.Failure);
        //TinySauce.OnGameFinished(isVictory,totalFruits,levelNumber:_currentLevel.ToString());
    }
}
