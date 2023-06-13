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

    //public Renderer cubeMeshRenderer;
    public CellController cellParent;

    public TextMeshPro numberTile;

    public void SetNumber(int num)
    {
        numberTile.text = num.ToString();
        if(num == 0)
        {
            numberTile.text = " ";
        }
    }
}