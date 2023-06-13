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
    // Start is called before the first frame update
    virtual protected void Start()
    {
        print("ttt");
        gameManager.currentSceneManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void OnSuccess()
    {
        TinySauce.OnGameFinished(isVictory, 0f);
        //MMVibrationManager.Haptic(HapticTypes.Success);
    }

    virtual public void OnGameOver()
    {
        //MMVibrationManager.Haptic(HapticTypes.Failure);
        TinySauce.OnGameFinished(isVictory, 0f);
    }
}
