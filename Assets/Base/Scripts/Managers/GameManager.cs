using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public SceneContextManager currentSceneManager;

    //Level
    public LevelManager _LevelManager;
    public int _currentLevel;
    public int levelToPlay;
    
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        
        Application.targetFrameRate = 60;

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }

    public static GameManager Instance {
        get {
            if(_instance is null) {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }

    
}
