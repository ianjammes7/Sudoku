using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
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

    public float timeValue;

    private void Update()
    {
        if (mainSceneManager.gameHasStarted && mainSceneManager.pausedGame == false) //starting the timer when game started and not paused
            timeValue += Time.deltaTime;

        if (mainSceneManager.isVictory)
        {
            float timeToWin = timeValue;

            float minutes = Mathf.FloorToInt(timeToWin / 60);
            float seconds = Mathf.FloorToInt(timeToWin % 60);

            mainSceneManager.uiManager.timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        DisplayTime(timeValue, mainSceneManager.uiManager.timeGameUIText);
    }

    public void DisplayTime(float timeToDisplay, TextMeshProUGUI textToEdit) //Correctly display the time in a good format
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textToEdit.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}