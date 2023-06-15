using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionController : MonoBehaviour
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


    [Header("UI Crosses")]
    public List<Image> imageCrosses = new List<Image>();
    public Color redCross;

    private int lives = 3;
    private int counterErrors = 0;

    public void WrongNumber()
    {
        if(counterErrors < lives)
        {
            imageCrosses[counterErrors].color = redCross;
            counterErrors++;
            lives--;
        }
        else
        {
            mainSceneManager.OnGameOver();
        }
    }





}