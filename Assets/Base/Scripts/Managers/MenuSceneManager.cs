using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public GameObject continueButton;
    
    private void Start()
    {
        if (GameManager.Instance.savedGame == 1)
        {
            continueButton.SetActive(true);
        }
    }
    
    public void OnPlayButtonClicked()
    {
        PlayerPrefs.SetInt("savedGame",0);
        GameManager.Instance.savedGame = PlayerPrefs.GetInt("savedGame");
    }
    
    public void OnContinueButtonClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnEasyGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.EASY;
        GameManager.Instance.gameModeString = "Easy";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnMediumGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.MEDIUM;
        GameManager.Instance.gameModeString = "Medium";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnHardGameClicked()
    {
        GameManager.Instance._GameMode = GAME_MODE.HARD;
        GameManager.Instance.gameModeString = "Hard";
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }



}
