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
        }
        
        Application.targetFrameRate = 60;
        
        _currentLevel = SaveManager.GetCurrentLevel();
        levelToPlay = SaveManager.GetLevelToPlay();
    }

    public static GameManager Instance {
        get {
            if(_instance is null) {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            currentSceneManager.OnSuccess();
        }
            
        if (Input.GetKeyDown("d"))
        {
            currentSceneManager.OnGameOver();
        }
        
#if UNITY_EDITOR
        if (Input.GetKeyDown("p")) 
        { 
            EditorApplication.isPaused = true;
        }
#endif
    }

    
}
