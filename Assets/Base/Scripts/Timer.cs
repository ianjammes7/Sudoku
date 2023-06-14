using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameCode
{
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

        public float timeValue = 0;

        private void Update()
        {
            if (mainSceneManager.gameHasStarted)
                timeValue += Time.deltaTime;

            if (mainSceneManager.isVictory)
            {
                float timeToWin = timeValue;

                float minutes = Mathf.FloorToInt(timeToWin / 60);
                float seconds = Mathf.FloorToInt(timeToWin % 60);

                mainSceneManager.uiManager.timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            }

            DisplayTime(timeValue);
        }

        void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            mainSceneManager.uiManager.timeGameUIText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
