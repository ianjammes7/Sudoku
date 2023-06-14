using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberButton : MonoBehaviour
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

    public int valueButton;

    public void OnNumberButtonClicked()
    {
        mainSceneManager._touchController.touchedTile.SetNumber(valueButton);
    }

 
}
