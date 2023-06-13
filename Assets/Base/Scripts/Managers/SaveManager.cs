using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetCurrentLevel(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    public static void SetLevelToPlay(int value)
    {
        PlayerPrefs.SetInt("LevelToPlay", value);
    }

    public static int GetLevelToPlay()
    {
        return PlayerPrefs.GetInt("LevelToPlay");
    }
}
