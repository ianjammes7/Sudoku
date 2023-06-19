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

    [Header("Game Modes")]
    [HideInInspector] public GAME_MODE _GameMode;
    [HideInInspector] public string gameModeString;
    [HideInInspector] public int savedGame;

    //Level
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

        _GameMode = GAME_MODE.NOT_SET;

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

        savedGame = PlayerPrefs.GetInt("savedGame", 0);
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

public enum GAME_MODE
{
    NOT_SET,
    EASY,
    MEDIUM,
    HARD
}
