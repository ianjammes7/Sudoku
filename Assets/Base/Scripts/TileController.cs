using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [HideInInspector] public CellController cellParent;
     public int correctNumber;
     public bool defaultValue;

    public TextMeshPro numberTile;

    public SpriteRenderer spriteTile;

    public List<GameObject> numbersNotes = new List<GameObject>();

    [Header("Colors")]
    public Color selectedTile;
    public Color sameLineColumnTile;
    public Color sameNumber;
    public Color correctTextColor;
    public Color wrongTextColor;

    public void SetNumber(int num)
    {
        for (int i = 0; i < numbersNotes.Count; i++) //Hiding all the notes of the tile
        {
            if(numbersNotes[i].activeSelf)
                numbersNotes[i].SetActive(false);
        }
        
        numberTile.text = num.ToString();
        if(num == 0) //Empty tile if it is a 0
        {
            numberTile.text = " ";
        }

        if (defaultValue)
            return;

        if (num != correctNumber && num != 0)
        {
            numberTile.color = wrongTextColor;
            mainSceneManager._solutionController.WrongNumber();
        }
        else
        {
            numberTile.color = correctTextColor;
            mainSceneManager._gridIndicator.HighlightSameNumberOnGrid(this);
        }

        mainSceneManager._GridController.CheckForCompleteSudoku();
    }
    
    public void SetNoteNumber(int num)
    {
        if(numbersNotes[num-1].activeSelf)
            numbersNotes[num-1].SetActive(false);
        else
            numbersNotes[num-1].SetActive(true);
    }

    public void SetCorrectNumber(int number)
    {
        correctNumber = number;
    }
}