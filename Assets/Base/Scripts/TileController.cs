using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public CellController cellParent;

    public TextMeshPro numberTile;

    [HideInInspector] public int correctNumber;

    public SpriteRenderer spriteTile;

    public bool defaultValue = false;

    [Header("Colors")]
    public Color selectedColorTile;
    public Color correctTextColor;
    public Color wrongTextColor;

    public void SetNumber(int num)
    {
        numberTile.text = num.ToString();
        if(num == 0)
        {
            numberTile.text = " ";
        }

        if (defaultValue)
            return;

        if (num != correctNumber)
        {
            numberTile.color = wrongTextColor;
            mainSceneManager._solutionController.WrongNumber();
        }
        else
        {
            numberTile.color = correctTextColor;
        }
    }

    public void SetCorrectNumber(int number)
    {
        correctNumber = number;
    }
}