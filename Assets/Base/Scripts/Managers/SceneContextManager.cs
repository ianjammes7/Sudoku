using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContextManager : MonoBehaviour
{
    //Vars
    public bool gameHasStarted;
    public bool isVictory;

    protected GameManager _gameManager;
    protected GameManager gameManager
    {
        get
        {
            if (_gameManager != null)
            {
                return _gameManager;
            }
            else
            {
                _gameManager = GameManager.Instance;
            }
            return _gameManager;
        }

        set
        {
            _gameManager = value;
        }
    }

    virtual protected void Start()
    {
        gameManager.currentSceneManager = this;
    }
}
